using System;
using System.IO;
using System.Windows.Forms;

namespace Burnout3EI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int magic;
            int totaldetextos;
            int ponteiro;
            ushort comparador = 0;
            int verificador;
            string todosOsTextos = "";
            int game = ' ';   //primeiro ponteiro do jogo
            int ppgame = ' '; //proximo ponteiro do jogo

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Burnout 3|*.BIN|Todos os arquivos (*.*)|*.*";
            openFileDialog1.Title = "Abrir arquivo de Burnout 3...";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int numerodearquivosabertos = openFileDialog1.FileNames.Length;

                foreach (String file in openFileDialog1.FileNames)
                {
                    using (BinaryReader br = new BinaryReader(File.OpenRead(file)))

                    {
                        magic = br.ReadInt32(); // Read magic

                        if (magic == 0x69F60308) // Magic do arquivo
                        {
                            game = 0x10;   //primeiro ponteiro do jogo - OBS: faço assim por ser mais fácil pra mim
                            ppgame = 0x14; //ppgame = proximo ponteiro do game
                        }
                        else
                        {
                            MessageBox.Show("O arquivo não é um arquivo de Burnout 3 válido!", "AVISO!");
                            return;
                        }

                        br.BaseStream.Seek(0x08, SeekOrigin.Begin);

                        totaldetextos = br.ReadInt32(); //Lê o total de textos

                        br.BaseStream.Seek(game, SeekOrigin.Begin);
                        
                        ponteiro = br.ReadInt32(); //Lê o valor do primeiro ponteiro

                        //Lê o primeiro texto pra ver se é nome ou texto
                        br.BaseStream.Seek(ponteiro - 2, SeekOrigin.Begin);

                        verificador = br.ReadUInt16();

                        for (int i = 0; i < totaldetextos; i++)
                        {
                            string convertido = ""; //Inicia a variavel convertido, que fará a conversão dos hex em textos


                            //Inicia a variavel que vai verificar se o texto acabou ou não
                            bool acabouotexto = false;

                            //Equanto não acabar o texto ele vai repetindo
                            while (acabouotexto == false)
                            {
                                //Lê um byte do texto
                                comparador = br.ReadUInt16();

                                //compara se o byte é a endstring
                                //se for, o programa cria uma nova linha
                                //se não continua pra proxima letra
                                if (comparador == 0x0000)
                                {
                                    //Quando chegar em uma endstring ele retorna como o texto tendo acabado (acabouotexto = verdadeiro)
                                    acabouotexto = true;

                                    todosOsTextos += "<end>\r\n";

                                    //Volta pra ler o próximo ponteiro
                                    br.BaseStream.Seek(ppgame + i * 4, SeekOrigin.Begin);

                                    //Lê o ponteiro
                                    ponteiro = br.ReadInt32();

                                    br.BaseStream.Seek(ponteiro, SeekOrigin.Begin);
                                }

                                else
                                {
                                    acabouotexto = false;

                                    //Começa a conversão dos caracteres
                                    if (comparador >= 0x30 && comparador <= 0x39)
                                    {
                                        //Convertendo dentro do intervalo de 0 e 9.
                                        convertido = ((char)('0' + (comparador - 0x30))).ToString();
                                    }
                                    else if (comparador >= 0x41 && comparador <= 0x5A)
                                    {
                                        //Convertendo dentro do intervalo de A e Z.
                                        convertido = ((char)('A' + (comparador - 0x41))).ToString();
                                    }
                                    else if (comparador >= 0x61 && comparador <= 0x7A)
                                    {
                                        //Convertendo dentro do intervalo de a e z.
                                        convertido = ((char)('a' + (comparador - 0x61))).ToString();
                                    }
                                    else if (comparador == 0x20)
                                    {
                                        convertido = ' '.ToString();
                                    }
                                    else if (comparador == 0x21)
                                    {
                                        convertido = ("!");
                                    }
                                    else if (comparador == 0x22)
                                    {
                                        convertido = '"'.ToString();
                                    }
                                    else if (comparador == 0x24)
                                    {
                                        convertido = ("$");
                                    }
                                    else if (comparador == 0x26)
                                    {
                                        convertido = ("&");
                                    }
                                    else if (comparador == 0x27)
                                    {
                                        convertido = ("'");
                                    }
                                    else if (comparador == 0x28)
                                    {
                                        convertido = ("(");
                                    }
                                    else if (comparador == 0x29)
                                    {
                                        convertido = (")");
                                    }
                                    else if (comparador == 0x2B)
                                    {
                                        convertido = ("+");
                                    }
                                    else if (comparador == 0x2C)
                                    {
                                        convertido = (",");
                                    }
                                    else if (comparador == 0x2D)
                                    {
                                        convertido = ("-");
                                    }
                                    else if (comparador == 0x2E)
                                    {
                                        convertido = (".");
                                    }
                                    else if (comparador == 0x2F)
                                    {
                                        convertido = ("/");
                                    }
                                    else if (comparador == 0x3A)
                                    {
                                        convertido = (":");
                                    }
                                    else if (comparador == 0x3C)
                                    {
                                        convertido = (";");
                                    }
                                    else if (comparador == 0x3F)
                                    {
                                        convertido = ("?");
                                    }
                                    else if (comparador == 0xC0)
                                    {
                                        convertido = ("À");
                                    }
                                    else if (comparador == 0xC1)
                                    {
                                        convertido = ("Á");
                                    }
                                    else if (comparador == 0xC2)
                                    {
                                        convertido = ("Â");
                                    }
                                    else if (comparador == 0xC3)
                                    {
                                        convertido = ("Ã");
                                    }
                                    else if (comparador == 0xC4)
                                    {
                                        convertido = ("Ä");
                                    }
                                    else if (comparador == 0xC7)
                                    {
                                        convertido = ("Ç");
                                    }
                                    else if (comparador == 0xC9)
                                    {
                                        convertido = ("É");
                                    }
                                    else if (comparador == 0xCA)
                                    {
                                        convertido = ("Ê");
                                    }
                                    else if (comparador == 0xCD)
                                    {
                                        convertido = ("Í");
                                    }
                                    else if (comparador == 0xD3)
                                    {
                                        convertido = ("Ó");
                                    }
                                    else if (comparador == 0xD4)
                                    {
                                        convertido = ("Ô");
                                    }
                                    else if (comparador == 0xD5)
                                    {
                                        convertido = ("Õ");
                                    }
                                    else if (comparador == 0xD6)
                                    {
                                        convertido = ("Ö");
                                    }
                                    else if (comparador == 0xDA)
                                    {
                                        convertido = ("Ú");
                                    }
                                    else if (comparador == 0xE0)
                                    {
                                        convertido = ("à");
                                    }
                                    else if (comparador == 0xE1)
                                    {
                                        convertido = ("á");
                                    }
                                    else if (comparador == 0xE2)
                                    {
                                        convertido = ("â");
                                    }
                                    else if (comparador == 0xE3)
                                    {
                                        convertido = ("ã");
                                    }
                                    else if (comparador == 0xE4)
                                    {
                                        convertido = ("ä");
                                    }
                                    else if (comparador == 0xE7)
                                    {
                                        convertido = ("ç");
                                    }
                                    else if (comparador == 0xE9)
                                    {
                                        convertido = ("é");
                                    }
                                    else if (comparador == 0xEA)
                                    {
                                        convertido = ("ê");
                                    }
                                    else if (comparador == 0xED)
                                    {
                                        convertido = ("í");
                                    }
                                    else if (comparador == 0xF3)
                                    {
                                        convertido = ("ó");
                                    }
                                    else if (comparador == 0xF4)
                                    {
                                        convertido = ("ô");
                                    }
                                    else if (comparador == 0xF5)
                                    {
                                        convertido = ("õ");
                                    }
                                    else if (comparador == 0xF6)
                                    {
                                        convertido = ("ö");
                                    }
                                    else if (comparador == 0xFA)
                                    {
                                        convertido = ("ú");
                                    }
                                    else if (comparador == 0x0143)
                                    {
                                        convertido = ("Ń");
                                    }
                                    else if (comparador == 0x0144)
                                    {
                                        convertido = ("ń");
                                    }
                                    else if (comparador == 0x0152)
                                    {
                                        convertido = ("Œ");
                                    }
                                    else if (comparador == 0x0153)
                                    {
                                        convertido = ("œ");
                                    }
                                    else if (comparador == 0x0178)
                                    {
                                        convertido = ("Ÿ");
                                    }
                                    else if (comparador == 0x2019)
                                    {
                                        convertido = ("’");
                                    }
                                    else if (comparador == 0x201C)
                                    {
                                        convertido = ("“");
                                    }
                                    else if (comparador == 0x201D)
                                    {
                                        convertido = ("”");
                                    }
                                    else if (comparador == 0x2026)
                                    {
                                        convertido = ("…");
                                    }
                                    else if (comparador == 0x2122)
                                    {
                                        convertido = ("™");
                                    }
                                    else
                                    {
                                        //Os valores que não estiverem na tabela, serão colocados em hex entre <>
                                        convertido = ("<" + comparador.ToString("X2") + ">"); //mostra o valor em hex com 2 algarismos
                                        //comparador.ToString("<" + comparador + ">"); comando para colocar o valor entre <> em decimal
                                    }
                                    //A variavel todosOsTextos recebe sempre a letra que acabou de converter
                                    //Após receber a letra q converteu, ele recebe a proxima, sem perder a anterior - essa é a função do sinal +=
                                    todosOsTextos += convertido;
                                }
                            }
                        }
                        //aqui já terminou de ler todos os textos, escreve o TXT com o texto dumpado
                        File.WriteAllText(Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file)) + ".txt", todosOsTextos);

                        todosOsTextos = "";

                    }
                }
                //Avisa que a extração dos textos terminou
                MessageBox.Show("Texto Extraido", "AVISO");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int pp = 0;

            OpenFileDialog OpenFileDilog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Burnout 3|*.BIN|Todos os arquivos (*.*)|*.*";
            openFileDialog1.Title = "Abrir arquivo de Burnout 3...";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                var dump = openFileDialog1.FileName;

                using (FileStream stream = File.Open(dump, FileMode.Open))
                {
                    BinaryReader br = new BinaryReader(stream);
                    BinaryWriter bw = new BinaryWriter(stream);

                    FileInfo file = new FileInfo(Path.Combine(Path.GetDirectoryName(dump), Path.GetFileNameWithoutExtension(dump)) + ".txt"); // Verifica se o txt existe
                    if (file.Exists)
                    {

                        string txt = File.ReadAllText(Path.Combine(Path.GetDirectoryName(dump), Path.GetFileNameWithoutExtension(dump)) + ".txt"); //se existe ele le o arquivo

                        string[] texto = txt.Split(new string[] { "<end>\r\n" }, StringSplitOptions.RemoveEmptyEntries); //pega o arquivo txt e guarda cada dialogo na variavel texto

                        br.BaseStream.Seek(0x10, SeekOrigin.Begin);

                        int ponteiro = br.ReadInt32(); //Lê o valor do primeiro ponteiro

                        int convertido = ' '; //inicia a variavel que vai converter de ASCII pros caracteres do jogo

                        for (int dialogo = 0; dialogo < texto.Length; dialogo++)
                        {
                            bw.BaseStream.Seek(0x10 + pp, SeekOrigin.Begin);

                            int novoponteiro;

                            novoponteiro = ponteiro;

                            bw.Write((uint)novoponteiro); //escreve o ponteiro

                            for (int c = 0; c < texto[dialogo].Length; c++)
                            {
                                bw.BaseStream.Seek(ponteiro, SeekOrigin.Begin);

                                char caractere = texto[dialogo][c];

                                if (caractere >= '0' && caractere <= '9')
                                {
                                    //Convertendo dentro do intervalo de a e z.
                                    convertido = (0x30 + (caractere - '0'));
                                }
                                else if (caractere >= 'A' && caractere <= 'Z')
                                {
                                    //Convertendo dentro do intervalo de A e Z.
                                    convertido = (0x41 + (caractere - 'A'));
                                }
                                else if (caractere >= 'a' && caractere <= 'z')
                                {
                                    //Convertendo dentro do intervalo de a e z.
                                    convertido = (0x61 + (caractere - 'a'));
                                }
                                else if (caractere == ' ')
                                {
                                    convertido = 0x20;
                                }
                                else if (caractere == '!')
                                {
                                    convertido = 0x21;
                                }
                                else if (caractere == '"')
                                {
                                    convertido = 0x22;
                                }
                                else if (caractere == '$')
                                {
                                    convertido = 0x24;
                                }
                                else if (caractere == '&')
                                {
                                    convertido = 0x26;
                                }
                                else if (caractere == '\'')
                                {
                                    convertido = 0x27;
                                }
                                else if (caractere == '(')
                                {
                                    convertido = 0x28;
                                }
                                else if (caractere == ')')
                                {
                                    convertido = 0x29;
                                }
                                else if (caractere == '+')
                                {
                                    convertido = 0x2B;
                                }
                                else if (caractere == ',')
                                {
                                    convertido = 0x2C;
                                }
                                else if (caractere == '-')
                                {
                                    convertido = 0x2D;
                                }
                                else if (caractere == '.')
                                {
                                    convertido = 0x2E;
                                }
                                else if (caractere == '/')
                                {
                                    convertido = 0x2F;
                                }
                                else if (caractere == ':')
                                {
                                    convertido = 0x3A;
                                }
                                else if (caractere == ';')
                                {
                                    convertido = 0x3C;
                                }
                                else if (caractere == '?')
                                {
                                    convertido = 0x3F;
                                }
                                else if (caractere == 'À')
                                {
                                    convertido = 0xC0;
                                }
                                else if (caractere == 'Á')
                                {
                                    convertido = 0xC1;
                                }
                                else if (caractere == 'Â')
                                {
                                    convertido = 0xC2;
                                }
                                else if (caractere == 'Ã')
                                {
                                    convertido = 0xC4;
                                }
                                else if (caractere == 'Ä')
                                {
                                    convertido = 0xC4;
                                }
                                else if (caractere == 'Ç')
                                {
                                    convertido = 0xC7;
                                }
                                else if (caractere == 'É')
                                {
                                    convertido = 0xC9;
                                }
                                else if (caractere == 'Ê')
                                {
                                    convertido = 0xCA;
                                }
                                else if (caractere == 'Í')
                                {
                                    convertido = 0xCD;
                                }
                                else if (caractere == 'Ó')
                                {
                                    convertido = 0xD3;
                                }
                                else if (caractere == 'Ô')
                                {
                                    convertido = 0xD4;
                                }
                                else if (caractere == 'Õ')
                                {
                                    convertido = 0xD5;
                                }
                                else if (caractere == 'Ö')
                                {
                                    convertido = 0xD6;
                                }
                                else if (caractere == 'Ú')
                                {
                                    convertido = 0xDA;
                                }
                                else if (caractere == 'à')
                                {
                                    convertido = 0xE0;
                                }
                                else if (caractere == 'á')
                                {
                                    convertido = 0xE1;
                                }
                                else if (caractere == 'â')
                                {
                                    convertido = 0xE2;
                                }
                                else if (caractere == 'ã')
                                {
                                    convertido = 0xE4;
                                }
                                else if (caractere == 'ä')
                                {
                                    convertido = 0xE4;
                                }
                                else if (caractere == 'ç')
                                {
                                    convertido = 0xE7;
                                }
                                else if (caractere == 'é')
                                {
                                    convertido = 0xE9;
                                }
                                else if (caractere == 'ê')
                                {
                                    convertido = 0xEA;
                                }
                                else if (caractere == 'í')
                                {
                                    convertido = 0xED;
                                }
                                else if (caractere == 'ó')
                                {
                                    convertido = 0xF3;
                                }
                                else if (caractere == 'ô')
                                {
                                    convertido = 0xF4;
                                }
                                else if (caractere == 'õ')
                                {
                                    convertido = 0xF5;
                                }
                                else if (caractere == 'ö')
                                {
                                    convertido = 0xF6;
                                }
                                else if (caractere == 'ú')
                                {
                                    convertido = 0xFA;
                                }
                                else if (caractere == 'Ń')
                                {
                                    convertido = 0x0143;
                                }
                                else if (caractere == 'ń')
                                {
                                    convertido = 0x0144;
                                }
                                else if (caractere == 'Œ')
                                {
                                    convertido = 0x0152;
                                }
                                else if (caractere == 'œ')
                                {
                                    convertido = 0x0153;
                                }
                                else if (caractere == 'Ÿ')
                                {
                                    convertido = 0x0178;
                                }
                                else if (caractere == '’')
                                {
                                    convertido = 0x2019;
                                }
                                else if (caractere == '“')
                                {
                                    convertido = 0x201C;
                                }
                                else if (caractere == '”')
                                {
                                    convertido = 0x201D;
                                }
                                else if (caractere == '…')
                                {
                                    convertido = 0x2026;
                                }
                                else if (caractere == '™')
                                {
                                    convertido = 0x2122;
                                }
                                else if (caractere == '<')
                                {
                                    string outputstring = "";

                                    string entresinal = texto[dialogo];
                                    int inicial = c;
                                    int final = entresinal.IndexOf('>', c + 1);
                                    outputstring = entresinal.Substring(inicial + 1, final - inicial - 1);
                                    ushort numero = Convert.ToUInt16(outputstring, 16);
                                    convertido = numero;
                                    c += 3;
                                }

                                bw.Write((ushort)convertido); //Escreve convertido
                                ponteiro += 2;
                            }
                            bw.Write((ushort)0x00); //escreve o endstring
                            ponteiro += 2;
                            pp += 4;
                        }

                    }
                }
                //Avisa que terminou
                MessageBox.Show("Texto Inserido", "AVISO");

            }
        }
    }
}
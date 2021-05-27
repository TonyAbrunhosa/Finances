using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace FinancasDesktop.Classes
{
    public static class LogEx
    {
        private static CultureInfo provider = new CultureInfo("pt-BR");
        private static string mensagemErro;
        private static string Path { get; set; } = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Log\";
        public static void GerarLog(this Exception ex)
        {

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            Log(ex, true, false);
        }
        public static void Log(Exception e, bool IconeErro = true, bool exibirErroTela = false)
        {
            try
            {
                mensagemErro = "Ocorreu um erro inesperado. \nDetalhes do erro: \n\n";
                switch (e.GetType().Name)
                {
                    case "NullReferenceException":
                        NullReferenceException ex = e as NullReferenceException;
                        LogNullReferenceException(ex);
                        break;                    
                    case "InvalidOperationException":
                        InvalidOperationException invalidOperationException = e as InvalidOperationException;
                        LogInvalidOperationException(invalidOperationException);
                        break;
                    default:
                        LogException(e);
                        break;
                }                
            }
            catch { /* ERRO NESSE PROCESSO, NÃO FAZER NADA */ }
        }

        public static void LogNullReferenceException(NullReferenceException ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Mensagem Erro: " + ex.Message);
            sb.AppendLine("Pilha de execução: " + ex.StackTrace);
            sb.AppendLine("Informações do Sistema: " + ex.Source);
            sb.AppendLine("Método: " + ex.TargetSite);
            MensagemErroException(ex, sb);
            mensagemErro += "Mensagem Erro: " + ex.Message;
        }

        public static void LogException(Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Nome completo do tipo da exceção não tratada: " + ex.GetType().FullName);
            sb.AppendLine("Mensagem Erro: " + ex.Message);
            sb.AppendLine("Pilha de execução: " + ex.StackTrace);
            sb.AppendLine("Informações do Sistema: " + ex.Source);
            sb.AppendLine("Método: " + ex.TargetSite);
            sb.AppendLine("InnerException: " + ex.InnerException);
            MensagemErroException(ex, sb);
            mensagemErro += "Mensagem Erro: " + ex.Message;
        }

        public static void LogInvalidOperationException(InvalidOperationException ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Exceção: " + ex.GetType().FullName);
            sb.AppendLine("Mensagem Erro: " + ex.Message);
            sb.AppendLine("Pilha de execução: " + ex.StackTrace);
            sb.AppendLine("Informações do Sistema: " + ex.Source);
            sb.AppendLine("Método: " + ex.TargetSite);
            sb.AppendLine("InnerException: " + ex.InnerException);

            MensagemErroException(ex, sb);
            mensagemErro += "Mensagem Erro: " + ex.Message;
        }

        public static void MensagemErroException(Exception ex, StringBuilder sb, string DetalheAdicional = "", bool rastrearPilhaCompleta = true)
        {
            string thisPath = "";
            string file = ("LogErro" + DateTime.Now.ToString("yyyyMMdd"));
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            #region Criar diretorio e arquivo
            if (!File.Exists(Path + file + ".txt"))
            {
                Directory.CreateDirectory(Path);

                using (File.CreateText(Path + file + ".txt"))
                {
                    thisPath = Path + file + ".txt";
                }
            }
            else
            {
                thisPath = Path + file + ".txt";
            }
            #endregion

            using (StreamWriter texto = new StreamWriter(thisPath, true))
            {
                texto.WriteLine("<<começo Exception " + date + ">>____________________________________________________________<<começo Exception " + date + ">>");
                texto.WriteLine("<<Versão: " + 1.0 + ">>");
                texto.WriteLine(ex.InnerException);
                texto.WriteLine(ex.Message);
                texto.WriteLine(sb.ToString());

                if (!string.IsNullOrEmpty(DetalheAdicional))
                {
                    texto.WriteLine(" ");
                    texto.WriteLine("Detalhe Adicional:");
                    texto.WriteLine(DetalheAdicional);
                    texto.WriteLine(" ");
                }

                if (rastrearPilhaCompleta && ex.StackTrace != null)
                {
                    texto.WriteLine("Stack Trace");
                    texto.WriteLine(ex.StackTrace);
                    var st = new System.Diagnostics.StackTrace(ex, true);
                    if (st != null)
                    {
                        texto.WriteLine("Linha: " + st.GetFrame(0).GetFileLineNumber());
                    }
                }
                texto.WriteLine("<<fim Mensagem---------------------->>____________________________________________________________<<fim Mesagem---------------------->>");
                texto.WriteLine(" ");
                texto.WriteLine(" ");
            }

        }

        public static void GeraLogSimples(string Mensagem)
        {
            string thisPath = "";
            string file = ("LogSimplpes" + DateTime.Now.ToString("yyyyMMdd"));
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            #region Criar diretorio e arquivo
            if (!File.Exists(Path + file + ".txt"))
            {
                Directory.CreateDirectory(Path);

                using (File.CreateText(Path + file + ".txt"))
                {
                    thisPath = Path + file + ".txt";
                }
            }
            else
            {
                thisPath = Path + file + ".txt";
            }
            #endregion

            using (StreamWriter texto = new StreamWriter(thisPath, true))
            {
                texto.WriteLine("<<começo Mensagem " + date + ">>____________________________________________________________<<começo Mensagem " + date + ">>");
                texto.WriteLine("<<Versão: " + 1.0 + ">>");
                texto.WriteLine(" ");
                texto.WriteLine(Mensagem);
                texto.WriteLine(" ");
                texto.WriteLine("<<fim Mensagem---------------------->>____________________________________________________________<<fim Mesagem---------------------->>");
                texto.WriteLine(" ");
                texto.WriteLine(" ");
            }
        }

        public static void GeraLogSimples(string Mensagem, string NomeArquivo)
        {
            string thisPath = "";
            string file = (NomeArquivo + DateTime.Now.ToString("yyyyMMdd"));
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            #region Criar diretorio e arquivo
            if (!File.Exists(Path + file + ".txt"))
            {
                Directory.CreateDirectory(Path);

                using (File.CreateText(Path + file + ".txt"))
                {
                    thisPath = Path + file + ".txt";
                }
            }
            else
            {
                thisPath = Path + file + ".txt";
            }
            #endregion

            using (StreamWriter texto = new StreamWriter(thisPath, true))
            {
                texto.WriteLine("<<começo Mensagem " + date + ">>____________________________________________________________<<começo Mensagem " + date + ">>");
                texto.WriteLine("<<Versão: " + 1.0 + ">>");
                texto.WriteLine(" ");
                texto.WriteLine(Mensagem);
                texto.WriteLine(" ");
                texto.WriteLine("<<fim Mensagem---------------------->>____________________________________________________________<<fim Mesagem---------------------->>");
                texto.WriteLine(" ");
                texto.WriteLine(" ");
            }
        }
    }
}

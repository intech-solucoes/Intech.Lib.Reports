using OpenHtmlToPdf;
using Scriban;
using System;
using System.IO;

namespace Intech.Lib.Reports
{
    public class IntechReport
    {
        public static byte[] Gerar(string caminhoarquivo)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "temp")))
                throw new Exception("Diretório temp não encontrado. É necessário criar o diretório na raiz do sistema e conceder permissão total ao usuário do IIS.");

            var file = File.OpenText(caminhoarquivo);

            var template = Template.Parse(file.ReadToEnd());
            var relatorio = template.Render();

            file.Close();

            var pdf = Pdf
                .From(relatorio)
                //.WithGlobalSetting("orientation", "Landscape")
                .WithObjectSetting("web.defaultEncoding", "utf-8")
                .WithObjectSetting("footer.right", "[page]/[topage]")
                .Content();

            return pdf;
        }
    }
}
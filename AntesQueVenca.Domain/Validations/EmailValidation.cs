using System.Text.RegularExpressions;

namespace AntesQueVenca.Domain.Validations
{
    public class EmailValidation
    {
        public static bool Valido(string email)
        {
            string emailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
                                  @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
                                  @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
                                  @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            Regex isEmail = new Regex(emailPattern);
            return isEmail.IsMatch(email);
        }

        public static bool CorporateValid(string email)
        {
            if (Valido(email))
            {
                string[] emails = GetNotCorporateEmails().Split(';');
                foreach (var item in emails)
                {
                    if (email.ToLower().Contains(item.ToLower()))
                        return false;
                }
                return true;
            }
            return false;
        }

        public static bool CorporateProfessionalReferenceValid(string email)
        {
            if (Valido(email))
            {
                string[] emails = GetNotCorporateEmails().Split(';');
                foreach (var item in emails)
                {
                    if (email.ToLower().Contains(item.ToLower()))
                        return false;
                }
                emails = GetNotProfessionalReferenceEmails().Split(';');
                foreach (var item in emails)
                {
                    if (email.ToLower().Contains(item.ToLower()))
                        return false;
                }
                return true;
            }
            return false;
        }

        public static string GetNotCorporateEmails()
        {
            return "@email.com,@teste.com,@test.com,@aol.com;@bol.com.br;@fastmail.com;@globo.com;@gmail.com;@gmx.com;@gmx.us;@hotmail.com;@hotmail.com.br;@icloud.com;@ig.com.br;@inbox.com;@inteligweb.com.br;@live.com;@mail.com;@msn.com;@oi.com.br;@outlook.com;@pop.com.br;@protonmail.com;@r7.com;@uol.com.br;@yahoo.com;@yahoo.com.br;@yandex.com;@ymail.com;@zipmail.com.br;@zoho.com";
        }

        public static string GetNotProfessionalReferenceEmails()
        {
            return "@numenit.com;@stefanini.com;@sonda.com;@conectaservicos.com.br;@evosolucoes.com.br;@infoa2.com.br;@meta.com.br;@doublemoorecom;@fh.com.br;@spro.com.br;@accenture.com;@plus-it.com.br;@deloitte.com;@wabr.com.br;@tcs.com;@pwc.com;@hunstman.com;@spsconsultoria.net.br;@it-cipriano.om;@softek.com;@g2tecnologia.com.br;@brsolution.com.br;@intelligenzait.com;@t-systems.com.br;@vdxsolutions.com;@gpartners.com.br;@ultracon.com.br;@coachit.com.br;@indracompany.com;@infosys.com;@cognizant.com;@vert.com.br;@atento.com;@stefanini.com;@decisiongroup.com.br;@oggettiva.com.br;@resource.com.br;@terra.com.br;@castgroup.com;@seidor.com;@neoris.com";
        }
    }
}

namespace DamoFullPrj.Models
{
    public class User
    {
  
            public string id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public override string ToString()
            {
                return $"{id}, name {firstName} {lastName}, email {email}";
            }
       

    }
}

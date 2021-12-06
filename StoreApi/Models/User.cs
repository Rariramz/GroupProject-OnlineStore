namespace StoreApi.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public string Email {  get; set; }
        public string PasswordHash {  get; set; }
        public string PasswordSalt {  get; set; }
        public string EmailCode {  get; set; }
        public bool IsAdmin {  get; set; }
        public bool IsVerified {  get; set; }
        public double Discount {  get; set; }
    }
}

namespace ApplicationAPI.Modelo
{
    public class Usuario
    {
        private int _id {  get; set; }
        private string _nombre {  get; set; }
        private string _email { get; set; }
        private string _password { get; set; }
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _nombre;} set { _nombre = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Password { get { return _password; } set { _password = value; } }

    }
}

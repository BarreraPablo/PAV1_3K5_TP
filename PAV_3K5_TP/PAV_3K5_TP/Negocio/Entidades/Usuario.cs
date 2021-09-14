namespace PAV_3K5_TP.Negocio.Entidades
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public int id_perfil { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string estado { get; set; }
        public bool borrado { get; set; }
    }
}

using PAV_3K5_TP.Datos;

namespace PAV_3K5_TP.Negocio.Servicios
{
    public class GestorUsuarios
    {
        private UnitOfWork usuarioDao;
        public GestorUsuarios()
        {
            usuarioDao = new UnitOfWork();

            usuarioDao.Open();
            usuarioDao.UsuarioDao.MetodoTest();
            usuarioDao.Dispose();
        }
    }
}

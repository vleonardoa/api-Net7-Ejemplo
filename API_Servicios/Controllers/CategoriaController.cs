using Microsoft.AspNetCore.Mvc;

namespace API_Servicios.Controllers
{
    public class CategoriaController : Controller
    {
        [HttpGet]
        [Route("ListarCategorias")]
        public ActionResult<object> ListarCategorias()
        {
            List<DataLayer.EntityModel.categoriaModel> ListC = new List<DataLayer.EntityModel.categoriaModel>();
            LogicLayer.Logica.CategoriaLog list = new LogicLayer.Logica.CategoriaLog();

            if (list.ListarCategorias(ref ListC))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ListC
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ListC
                });
            }

        }
        [HttpGet]
        [Route("ListarCategoriasSP")]
        public ActionResult<object> ListarCategoriasSP()
        {
            List<DataLayer.EntityModel.categoriaModel> ListC = new List<DataLayer.EntityModel.categoriaModel>();
            LogicLayer.Logica.CategoriaLog list = new LogicLayer.Logica.CategoriaLog();

            if (list.ListarCategoriasSP(ref ListC))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ListC
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ListC
                });
            }

        }
        [HttpPut]
        [Route("desactivarCategoriaSP")]
        public ActionResult<object> desactivarCategoriaSP(string id_cat,int estado)
        {
            DataLayer.EntityModel.categoriaModel ListC = new DataLayer.EntityModel.categoriaModel();
            LogicLayer.Logica.CategoriaLog del = new LogicLayer.Logica.CategoriaLog(id_cat,estado);

            if (del.desactivarCategoriaSP(ref ListC))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ListC.Mensaje
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ListC.Mensaje
                });
            }

        }

    }
}

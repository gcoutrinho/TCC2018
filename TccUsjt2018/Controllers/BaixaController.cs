using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.ProdutoCategoria;

namespace TccUsjt2018.Controllers
{
    public class BaixaController : Controller 
    {
        // GET: Produto
        
        [Authorize]
        public IEnumerable<SelectListItem> GetBaixas()
        {
            var dao = new BaixaDAO();
            var baixa = dao.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.CodigoBaixa.ToString(),
                });

            return new SelectList(baixa, "Value");
        }

        
    }
}


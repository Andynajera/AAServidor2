
using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class OrderController : ControllerBase
{

    private readonly DataContext _context;

    public OrderController(DataContext dataContext)
    {
        _context = dataContext;
    }

    /// <summary>
    /// Mostrar todas las promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="200">Devuelve el listado de promociones</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    public ActionResult<List<OrderPro>> Get()
    {
        List<OrderPro> pago = _context.OrderPros.ToList();


        return pago == null ? NotFound()
              : Ok(pago);
    }
        [HttpGet]
    [Route("name")] 
    public ActionResult<User> Get(string name )
    {
        
  List<User> user =_context.User.Where(x=>x.name.Contains(name)).OrderByDescending(x=>x.name).ToList();
        //buscar por nombre   
        return user == null? NotFound()
            : Ok(user);
    }
    /// <summary>
    /// Buscar por id
    /// </summary>
    /// <returns>Todas las promociones con el mismo id</returns>
    /// <response code="200">Devuelve el listado de promociones con este id</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult  GetByUserId(int id)
    {
        var  list = _context.OrderPros.Where(e => e.UserId==id).ToList();
    if (list.Count==0){
        return NotFound();
        
    }
    else{
        var lista= new List<Pago>() ;
        list.ForEach((Action<OrderPro>)(e=>{
            var pago= Queryable.Where<Pago>(_context.Pagos, a => a.id == e.PagoId).FirstOrDefault<Pago>();
            lista.Add(pago);
        }));
        return Ok(lista);
    }
   
    }

    /// <summary>
    /// añadir promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<OrderPro> Post([FromBody] OrderPro orderPro)
    {
        if (orderPro==null){
            return BadRequest();
        }
        else{
            var pagoId=_context.User.Find(orderPro.PagoId);//Bucamos en el context si el id de promociones existe;
            var userId=_context.User.Find(orderPro.UserId);//Bucamos en el context si el id de promociones existe;
            if(pagoId==null || userId==null ){
                return NotFound();
            }
            else{
                 _context.OrderPros.Add(orderPro);
        _context.SaveChanges();
         string resourceUrl = Request.Path.ToString() + "/" + orderPro.id;
        return Created(resourceUrl, orderPro);
            }
        }
       

       
        
    }
    
}

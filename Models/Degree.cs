namespace Classes;

    //Clase
 public class Degree{
    //Propiedades
     public int id {get;set;}
    public string name { get;set; }
    public string nameDegree { get;set; }
    public bool quedanPlazas { get;set; }
    public DateTime  dataExpediente { get;set; }
    public decimal price { get;set; }
    public int cantidadPlazas { get;set; }
    
/*
//Contructor
    //si no incicializas en el contructor las propiedades no aparecera por consola
    public Degree(string name,string nameDegree, bool quedanPlazas,DateTime dataExpediente, decimal price,int cantidadPlazas){
        this.name=name;
        this.nameDegree=nameDegree;
        this.quedanPlazas=quedanPlazas;
        this.dataExpediente=dataExpediente;
        this.price=price;
        this.cantidadPlazas=cantidadPlazas;
    }*/

    //Cambia si hay plazas 
    public void ChangeGender(){
        quedanPlazas=!quedanPlazas;
    }
//Resumen de todos los parametros
//Cambia las propiedades a string con el toString
    public string Summary(){
        return name+" "+nameDegree+" "+quedanPlazas.ToString()+" "+dataExpediente.ToString()+" "+price.ToString()+" "+ cantidadPlazas.ToString();
    }
 }


 
namespace Classes;

//Clase
 public class User{
    
//Propiedades

   public int id {get;set;}
    public string? name {get;set;}
    public string? password {get;set;}
    public string? email {get;set;}
    //public string? password {get;set;}
    public string? nameDegree {get;set;}
    public bool gender {get;set;}
    public DateTime  matriculacion {get;set;}
    public int asignaturaMAtriculadas {get;set;}
    public decimal notas {get;set;}


 
 


//Resumen de todos los parametros
//Cambia las propiedades a string con el toString
    public string Summary(){
 return  matriculacion.ToString("MM/dd/yyyy")+" ";    }
 }


 
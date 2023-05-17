namespace Classes;

//Clase
public class Pago

//propiedades

{
    public int id {get;set;}
    public string name{set;get;}
    public decimal price { get;set; }
    public decimal total { get;set; }
    public bool pagado { get;set; }
    public DateTime date { get;set; }
    public string notes { get;set; }
}

/*
public string Summary(){
        return notes+" "+price.ToString()+" "+total.ToString()+" "+pagado.ToString()+" "+ date.ToString();
    }
}*/

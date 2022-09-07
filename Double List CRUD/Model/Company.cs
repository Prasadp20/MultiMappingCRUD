namespace Double_List_CRUD.Model
{
    public class Company
    {
        /*  cId INT IDENTITY(1,1) PRIMARY KEY,
            cName VARCHAR(255),
            cAddress VARCHAR(255)*/
        public int cId { get; set; }
        public string? cName { get; set; }
        public string? cAddress { get; set; }
        public List<Employee> employee { get; set; } = new List<Employee>();
    }
}

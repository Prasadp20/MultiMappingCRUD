namespace Double_List_CRUD.Model
{
    public class Employee
    {
        /*  eId INT IDENTITY(101,1) PRIMARY KEY,
            cId INT,
            empName VARCHAR(255),
            empSalary INT*/
        public int eId { get; set; }
        public int cId { get; set; }
        public string? empName { get; set; }
        public int empSalary { get; set; }

    } //eId,cId,empName,empSalary
}

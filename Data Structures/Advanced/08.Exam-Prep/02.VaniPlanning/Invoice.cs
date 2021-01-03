namespace _02.VaniPlanning
{
    using System;

    public class Invoice : IComparable<Invoice>
    {
        public Invoice(string number, string company, double subtotal, Department dep, DateTime issueDate, DateTime dueDate)
        {
            this.SerialNumber = number;
            this.CompanyName = company;
            this.Subtotal = subtotal;
            this.Department = dep;
            this.IssueDate = issueDate;
            this.DueDate = dueDate;
        }
        public string SerialNumber { get; set; }

        public string CompanyName { get; set; }

        public double Subtotal { get; set; }

        public Department Department { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public int CompareTo(Invoice other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var serialNumberComparison = string.Compare(SerialNumber, other.SerialNumber, StringComparison.Ordinal);
            if (serialNumberComparison != 0) return serialNumberComparison;
            var companyNameComparison = string.Compare(CompanyName, other.CompanyName, StringComparison.Ordinal);
            if (companyNameComparison != 0) return companyNameComparison;
            var subtotalComparison = Subtotal.CompareTo(other.Subtotal);
            if (subtotalComparison != 0) return subtotalComparison;
            var departmentComparison = Department.CompareTo(other.Department);
            if (departmentComparison != 0) return departmentComparison;
            var issueDateComparison = IssueDate.CompareTo(other.IssueDate);
            if (issueDateComparison != 0) return issueDateComparison;
            return DueDate.CompareTo(other.DueDate);
        }

        public override bool Equals(object obj)
        {
            Invoice other = obj as Invoice;
            ;
            if (other == null)
            {
                return false;
            }

            return other.SerialNumber == this.SerialNumber;


        }

        public override int GetHashCode()
        {
            return SerialNumber.GetHashCode();
        }
    }
}

using System.Linq;

namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> _invoices = new Dictionary<string, Invoice>();


        public void Create(Invoice invoice)
        {
            if (Contains(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }
            _invoices[invoice.SerialNumber] = invoice;
        }

        public void ThrowInvoice(string number)
        {
            if (!Contains(number))
            {
                throw new ArgumentException();
            }

            _invoices.Remove(number);
        }

        public void ThrowPayed()
        {
            var invoices = _invoices.Where(i => Math.Abs(i.Value.Subtotal) < 0.001).ToArray();
            foreach (var invoice in invoices)
            {
                _invoices.Remove(invoice.Key);
            }

        }

        public int Count()
        {
            return _invoices.Count;
        }

        public bool Contains(string number)
        {
            return _invoices.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            var invoices = _invoices.Where(i => i.Value.DueDate == due).ToArray();
            if (invoices.Length == 0)
            {
                throw new ArgumentException();
            }
            foreach (var invoice in invoices)
            {
                invoice.Value.Subtotal = 0;
            }

        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return _invoices.Values
                .Where(c => c.IssueDate >= start && c.IssueDate <= end)
                .OrderBy(i => i.IssueDate)
                .ThenBy(i => i.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var result = _invoices.Values
                .Where(c => c.SerialNumber.Contains(serialNumber))
                .OrderByDescending(i => i.SerialNumber);
            if (!result.Any())
            {
                throw new ArgumentException();
            }
          
            return result;
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var result = _invoices.Values
                .Where(c => c.DueDate > start && c.DueDate < end);
            if (!result.Any())
            {
                throw new ArgumentException();
            }
            foreach (var invoice in result)
            {
                _invoices.Remove(invoice.SerialNumber);
            }
            return result;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return _invoices.Values
                .Where(c => c.Department == department)
                .OrderByDescending(i => i.Subtotal)
                .ThenBy(i => i.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            return _invoices.Values
                .Where(c => c.CompanyName == company)
                .OrderByDescending(i => i.SerialNumber);

        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            var result = _invoices.Values
                .Where(c => c.DueDate == dueDate).ToArray();
            if (!result.Any())
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i].DueDate = result[i].DueDate.AddDays(days);
            }
        }
    }
}

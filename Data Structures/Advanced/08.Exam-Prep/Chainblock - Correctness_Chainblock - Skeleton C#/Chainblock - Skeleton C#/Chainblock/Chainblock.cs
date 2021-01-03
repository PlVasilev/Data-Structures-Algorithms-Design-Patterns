using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    private Dictionary<int, Transaction> _transactions = new Dictionary<int, Transaction>();
    private Dictionary<string, LinkedList<Transaction>> _bySenderIncertion = new Dictionary<string, LinkedList<Transaction>>();
    private Dictionary<TransactionStatus, HashSet<Transaction>> _byStatus = new Dictionary<TransactionStatus, HashSet<Transaction>>();
    private SortedSet<Transaction> _byAmount = new SortedSet<Transaction>();

    public int Count => _transactions.Count;

    public void Add(Transaction tx)
    {
        if (Contains(tx)) return;
        _transactions[tx.Id] = tx;
  
        _byAmount.Add(tx);
        if (!_bySenderIncertion.ContainsKey(tx.From))
        {
            _bySenderIncertion[tx.From] = new LinkedList<Transaction>();
        }
        _bySenderIncertion[tx.From].AddLast(tx);

        if (!_byStatus.ContainsKey(tx.Status))
        {
            _byStatus[tx.Status] = new HashSet<Transaction>();
        }
        _byStatus[tx.Status].Add(tx);

    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!Contains(id))
        {
            throw new ArgumentException();
        }
        var current = _transactions[id];

        _byStatus[current.Status].Remove(current);
        current.Status = newStatus;
        if (!_byStatus.ContainsKey(newStatus))
        {
            _byStatus[newStatus] = new HashSet<Transaction>();
        }

        _byStatus[newStatus].Add(current);
    }

    public bool Contains(Transaction tx)
    {
        return _transactions.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return _transactions.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return _transactions.Values.Where(x =>  x.Amount >= lo && x.Amount <= hi);
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return _byAmount;
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        if (!_byStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return _byStatus[status].OrderByDescending(x => x.Amount).Select(r => r.To);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        if (!_byStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }
        return _byStatus[status].OrderByDescending(x => x.Amount).Select(r => r.From);
    }

    public Transaction GetById(int id)
    {
        if (!Contains(id))
        {
            throw new InvalidOperationException();
        }

        return _transactions[id];

    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = _byAmount.Where(x => x.Amount >= lo && x.Amount < hi && x.To == receiver);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = _byAmount.Where(x => x.To == receiver);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        if (!_bySenderIncertion.ContainsKey(sender))
        {
            throw new InvalidOperationException();
        }
        return _bySenderIncertion[sender].Where(x => x != null && x.Amount > amount).OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        if (!_bySenderIncertion.ContainsKey(sender))
        {
            throw new InvalidOperationException();
        }
        return _bySenderIncertion[sender].Where(x => x != null).OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        if (!_byStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return _byStatus[status].OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        if (!_byStatus.ContainsKey(status))
        {
            return Enumerable.Empty<Transaction>();
        }

        return _byStatus[status].Where(x => x.Amount <= amount).OrderByDescending(x => x.Amount);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        return _transactions.Values.Where(x => x != null).GetEnumerator();
    }

    public void RemoveTransactionById(int id)
    {
        if (!Contains(id))
        {
            throw new InvalidOperationException();
        }

        var toRemove = _transactions[id];
        _transactions.Remove(id);
        _byAmount.Remove(toRemove);

        _bySenderIncertion[toRemove.From].Remove(toRemove);
        if (_bySenderIncertion[toRemove.From].Count == 0)
        {
            _bySenderIncertion.Remove(toRemove.From);
        }

        _byStatus[toRemove.Status].Remove(toRemove);
        if (_byStatus[toRemove.Status].Count == 0)
        {
            _byStatus.Remove(toRemove.Status);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}






//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Wintellect.PowerCollections;

//public class Chainblock : IChainblock
//{
//    private Dictionary<int, Transaction> _transactions = new Dictionary<int, Transaction>();
//    private Dictionary<TransactionStatus, HashSet<Transaction>> _byStatus = new Dictionary<TransactionStatus, HashSet<Transaction>>();
//    private LinkedList<Transaction> _byInsertionOrder = new LinkedList<Transaction>();
//    private SortedSet<Transaction> _byAmount = new SortedSet<Transaction>();
//    private Dictionary<string, HashSet<Transaction>> _bySender = new Dictionary<string, HashSet<Transaction>>();

//    public int Count => _transactions.Count;

//    public void Add(Transaction tx)
//    {
//        if (Contains(tx)) return;
//        _transactions[tx.Id] = tx;
//        _byInsertionOrder.AddLast(tx);
//        _byAmount.Add(tx);
//        if (!_bySender.ContainsKey(tx.From))
//        {
//            _bySender[tx.From] = new HashSet<Transaction>();
//        }
//        _bySender[tx.From].Add(tx);
//        if (!_byStatus.ContainsKey(tx.Status))
//        {
//            _byStatus[tx.Status] = new HashSet<Transaction>();
//        }
//        _byStatus[tx.Status].Add(tx);

//    }

//    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
//    {
//        if (!Contains(id))
//        {
//            throw new ArgumentException();
//        }
//        var current = _transactions[id];

//        _byStatus[current.Status].Remove(current);
//        current.Status = newStatus;
//        if (!_byStatus.ContainsKey(newStatus))
//        {
//            _byStatus[newStatus] = new HashSet<Transaction>();
//        }

//        _byStatus[newStatus].Add(current);
//    }

//    public bool Contains(Transaction tx)
//    {
//        return _transactions.ContainsKey(tx.Id);
//    }

//    public bool Contains(int id)
//    {
//        return _transactions.ContainsKey(id);
//    }

//    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
//    {
//        return _byInsertionOrder.Where(x => x != null && x.Amount >= lo && x.Amount <= hi);
//    }

//    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
//    {
//        return _byAmount;
//    }

//    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
//    {
//        if (!_byStatus.ContainsKey(status))
//        {
//            throw new InvalidOperationException();
//        }

//        return _byStatus[status].OrderByDescending(x => x.Amount).Select(r => r.To);
//    }

//    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
//    {
//        if (!_byStatus.ContainsKey(status))
//        {
//            throw new InvalidOperationException();
//        }
//        return _byStatus[status].OrderByDescending(x => x.Amount).Select(r => r.From);
//    }

//    public Transaction GetById(int id)
//    {
//        if (!Contains(id))
//        {
//            throw new InvalidOperationException();
//        }

//        return _transactions[id];

//    }

//    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
//    {
//        var result = _byAmount.Where(x => x.Amount >= lo && x.Amount < hi && x.To == receiver);
//        if (!result.Any())
//        {
//            throw new InvalidOperationException();
//        }

//        return result;
//    }

//    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
//    {
//        var result = _byAmount.Where(x => x.To == receiver);
//        if (!result.Any())
//        {
//            throw new InvalidOperationException();
//        }

//        return result;
//    }

//    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
//    {
//        if (!_bySender.ContainsKey(sender))
//        {
//            throw new InvalidOperationException();
//        }

//        return _bySender[sender]
//            .Where(x => x.Amount >= amount)
//            .OrderByDescending(x => x.Amount);

//    }

//    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
//    {
//        if (!_bySender.ContainsKey(sender))
//        {
//            throw new InvalidOperationException();
//        }

//        return _bySender[sender]
//            .OrderByDescending(x => x.Amount);
//        //.ThenByDescending(x => x.Id);
//    }

//    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
//    {
//        if (!_byStatus.ContainsKey(status))
//        {
//            throw  new InvalidOperationException();
//        }

//        return _byStatus[status].OrderByDescending(x => x.Amount);
//    }

//    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
//    {
//        if (!_byStatus.ContainsKey(status))
//        {
//            return Enumerable.Empty<Transaction>();
//        }

//        return _byStatus[status].Where(x => x.Amount <= amount).OrderByDescending(x => x.Amount);
//    }

//    public IEnumerator<Transaction> GetEnumerator()
//    {
//        return _transactions.Values.Where(x=> x !=  null).GetEnumerator();
//    }

//    public void RemoveTransactionById(int id)
//    {
//        if (!Contains(id))
//        {
//            throw new InvalidOperationException();
//        }

//        var toRemove = _transactions[id];
//        _byInsertionOrder.Remove(toRemove);
//        _transactions.Remove(id);
//        _byAmount.Remove(toRemove);
//        _byStatus[toRemove.Status].Remove(toRemove);
//        _bySender[toRemove.From].Remove(toRemove);
//        if (_bySender[toRemove.From].Count == 0)
//        {
//            _bySender.Remove(toRemove.From);
//        }
//        if (_byStatus[toRemove.Status].Count == 0)
//        {
//            _byStatus.Remove(toRemove.Status);
//        }
//        toRemove = null;
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }

//    internal class ComputerComparerNumber : IComparer<Transaction>
//    {
//        public int Compare(Transaction x, Transaction y)
//        {
//            var result = (int)(y.Amount * 100 - x.Amount * 100);
//            if (result == 0)
//            {
//                result = y.Id - x.Id;
//            }

//            return result;
//        }
//    }
//}


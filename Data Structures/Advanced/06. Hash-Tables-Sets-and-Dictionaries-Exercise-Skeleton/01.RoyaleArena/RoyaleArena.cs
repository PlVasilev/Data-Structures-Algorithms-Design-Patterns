using System.Linq;

namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class RoyaleArena : IArena
    {
        private Dictionary<int, BattleCard> _deck = new Dictionary<int, BattleCard>();
        
        private Dictionary<CardType, Table<BattleCard>> _cardTypeSortedByDamage = new Dictionary<CardType, Table<BattleCard>>();
        private Dictionary<string, Table<BattleCard>> _nameSortBySwag = new Dictionary<string, Table<BattleCard>>();
        private Table<BattleCard> _sortBySwag = new Table<BattleCard>(new SwagIndex());

        public void Add(BattleCard card)
        {
            _deck[card.Id] = card;
            AddToSearchableCollection<DamageIndex>(_cardTypeSortedByDamage, card, (c) => c.Type);
            AddToSearchableCollection<SwagIndex>(_nameSortBySwag, card, (c) => c.Name);
            _sortBySwag.Add(card);
        }

        private void AddToSearchableCollection<T>(IDictionary dict, BattleCard card, Func<BattleCard, object> getKey)
        where T: Index<double>, new()
        {
            var key = getKey(card);
            if (dict[key] == null)
            {
                dict[key] = new Table<BattleCard>(new T());
            }

            (dict[key] as Table<BattleCard>).Add(card);
        }

        public bool Contains(BattleCard card) => _deck.ContainsKey(card.Id);
        

        public int Count => _deck.Keys.Count;

        public void ChangeCardType(int id, CardType type)
        {
            if (!_deck.ContainsKey(id)) 
                throw new InvalidOperationException();
            RemoveFromSearchableCollection(_cardTypeSortedByDamage, _deck[id], c => c.Damage);
            _deck[id].Type = type;
            AddToSearchableCollection<DamageIndex>(_cardTypeSortedByDamage, _deck[id], (c) => c.Type);
        }

        private void RemoveFromSearchableCollection(IDictionary dict, BattleCard card, Func<BattleCard, object> getKey)
        {
            if (!_deck.ContainsKey(card.Id))
                throw new InvalidOperationException();
            var key = getKey(card);
            if (dict[key] != null)
            {
                var items = (dict[key] as Table<BattleCard>);
                items.Remove(card);
                if (items.Count() == 0)
                {
                    dict.Remove(key);
                }
            }
        }

        public BattleCard GetById(int id)
        {
            if (!_deck.ContainsKey(id))
                throw new InvalidOperationException();
            return _deck[id];
        }

        public void RemoveById(int id)
        {
            if (!_deck.ContainsKey(id))
                throw new InvalidOperationException();
            RemoveFromSearchableCollection(_cardTypeSortedByDamage, _deck[id], c => c.Type);
            RemoveFromSearchableCollection(_nameSortBySwag, _deck[id], c => c.Name);
            _sortBySwag.Remove(_deck[id]);
            _deck.Remove(id);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            if(!_cardTypeSortedByDamage.ContainsKey(type))
                throw new InvalidOperationException();
            return _cardTypeSortedByDamage[type];
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            if (!_cardTypeSortedByDamage.ContainsKey(type))
                throw new InvalidOperationException();
            return _cardTypeSortedByDamage[type]?
                .GetViewBetween(lo,hi)
                .OrderBy(c => c);
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            if (!_cardTypeSortedByDamage.ContainsKey(type))
                throw new InvalidOperationException();
            return _cardTypeSortedByDamage[type]?
                .GetViewBetween(_cardTypeSortedByDamage[type].MinKey, damage)
                .OrderBy(c => c);
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            if (!_nameSortBySwag.ContainsKey(name))
                throw new InvalidOperationException();
            return _nameSortBySwag[name].Reverse();
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            if (!_nameSortBySwag.ContainsKey(name))
                throw new InvalidOperationException();
            return _nameSortBySwag[name]?.GetViewBetween(lo, hi);
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (_deck.Count < n)
            {
                throw new InvalidOperationException();
            }
            return _sortBySwag.GetFirstN(n, c => c.Id);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            return _sortBySwag.GetViewBetween(lo, hi);
        }


        public IEnumerator<BattleCard> GetEnumerator()
        {
            return _deck.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
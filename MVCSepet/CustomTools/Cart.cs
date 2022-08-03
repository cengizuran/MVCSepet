using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepetTekrar_1.CustomTools
{
    public class Cart
    {
        Dictionary<int, CartItem> _sepetUrunler;
        public Cart()
        {
            _sepetUrunler = new Dictionary<int, CartItem>();
        }

        public List<CartItem> Sepetim 
        {
            get { return _sepetUrunler.Values.ToList(); }
        }

        public void SepeteEkle(CartItem item)
        {
            if (_sepetUrunler.ContainsKey(item.ID))
            {
                _sepetUrunler[item.ID].Amount += 1;
                return;
            }

            _sepetUrunler.Add(item.ID, item);
        }

        public void SepettenSil(int id )
        {
            if (_sepetUrunler[id].Amount>1)
            {
                _sepetUrunler[id].Amount -= 1;
                return;
            }
            _sepetUrunler.Remove(id);
        }

        public decimal? TotalPrice 
        {
            get { return _sepetUrunler.Sum(x => x.Value.SubTotal); }
        }
    }
}
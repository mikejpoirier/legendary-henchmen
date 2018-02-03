using System.Collections.Generic;

namespace Henchman
{
    public class HenchmanBLC : IHenchmanBLC
    {
        private IHenchmanDAO _dao;

        public HenchmanBLC(IHenchmanDAO dao)
        {
            _dao = dao;
        }

        public List<Henchman> GetHenchmen()
        {
            return _dao.GetHenchmen();
        }

        public Henchman GetHenchman(string name)
        {
            return _dao.GetHenchman(name);
        }

        public Henchman PostHenchman(Henchman henchman)
        {
            var result = _dao.GetHenchman(henchman.Name);

            if(result == null)
                return _dao.InsertHenchman(henchman);
            else
                return _dao.UpdateHenchman(henchman);
        }
        
        public List<Henchman> PostHenchmen(List<Henchman> henchmen)
        {
            var result = new List<Henchman>();

            foreach(var henchman in henchmen)
            {
                var exists = _dao.GetHenchman(henchman.Name);

                if(exists == null)
                    result.Add(_dao.InsertHenchman(henchman));
                else
                    result.Add(_dao.UpdateHenchman(henchman));
            }

            return result;
        }
    }
}
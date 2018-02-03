using System.Collections.Generic;

namespace Henchman
{
    public interface IHenchmanBLC
    {
        List<Henchman> GetHenchmen();
        Henchman GetHenchman(string name);
        Henchman PostHenchman(Henchman henchman);
        List<Henchman> PostHenchmen(List<Henchman> henchmen);
    }
}
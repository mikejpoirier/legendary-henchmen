using System.Collections.Generic;

namespace Henchman
{
    public interface IHenchmanDAO
    {
        Henchman GetHenchman(string name);
        List<Henchman> GetHenchmen();
        Henchman InsertHenchman(Henchman henchman);
        Henchman UpdateHenchman(Henchman henchman);
    }
}
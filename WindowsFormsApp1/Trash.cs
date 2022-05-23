using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Trash
    {
        List<Card> trash;
        public Trash()
        {
            trash = new List<Card>();
        }
        public void gotoTrash(Card card)
        {
            trash.Add(card);
        }
        public List<Card> getTrashList()
        {
            return trash;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Trash
    {
        List<string> trash;
        public Trash()
        {
            trash = new List<string>();
        }
        public void gotoTrash(string card)
        {
            trash.Add(card);
        }
        public List<string> getTrashList()
        {
            return trash;
        }
    }
}

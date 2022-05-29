using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WindowsFormsApp1
{
    class Image_Class
    {
        public Bitmap back { get; }
        public Bitmap copper { get; }
        public Bitmap cellar { get; }
        public Bitmap curse { get; }
        public Bitmap duchy { get; }
        public Bitmap estate { get; }
        public Bitmap gold { get; }
        public Bitmap market { get; }
        public Bitmap merchant { get; }
        public Bitmap mine { get; }
        public Bitmap moat { get; }
        public Bitmap province { get; }
        public Bitmap remodel { get; }
        public Bitmap silver { get; }
        public Bitmap smithy { get; }
        public Bitmap village { get; }
        public Bitmap witch { get; }
        public Bitmap workshop { get; }
        public Bitmap Png1 { get; }
        public Bitmap HandBackgroundpng { get; }
        public Bitmap Logpng { get; }
        public Bitmap BonoBonopng { get; }
        public Bitmap Cow_gif { get; }

        public Image_Class()
        {
            back = new Bitmap(Directory.GetCurrentDirectory() + "\\back.png");
            copper = new Bitmap(Directory.GetCurrentDirectory() + "\\copper.png");
            cellar = new Bitmap(Directory.GetCurrentDirectory() + "\\cellar.png");
            curse = new Bitmap(Directory.GetCurrentDirectory() + "\\curse.png");
            duchy = new Bitmap(Directory.GetCurrentDirectory() + "\\duchy.png");
            estate = new Bitmap(Directory.GetCurrentDirectory() + "\\estate.png");
            gold = new Bitmap(Directory.GetCurrentDirectory() + "\\gold.png");
            market = new Bitmap(Directory.GetCurrentDirectory() + "\\market.png");
            merchant = new Bitmap(Directory.GetCurrentDirectory() + "\\merchant.png");
            mine = new Bitmap(Directory.GetCurrentDirectory() + "\\mine.png");
            moat = new Bitmap(Directory.GetCurrentDirectory() + "\\moat.png");
            province = new Bitmap(Directory.GetCurrentDirectory() + "\\province.png");
            remodel = new Bitmap(Directory.GetCurrentDirectory() + "\\remodel.png");
            silver = new Bitmap(Directory.GetCurrentDirectory() + "\\silver.png");
            smithy = new Bitmap(Directory.GetCurrentDirectory() + "\\smithy.png");
            village = new Bitmap(Directory.GetCurrentDirectory() + "\\village.png");
            witch = new Bitmap(Directory.GetCurrentDirectory() + "\\witch.png");
            workshop = new Bitmap(Directory.GetCurrentDirectory() + "\\workshop.png");
            Png1 = new Bitmap(Directory.GetCurrentDirectory() + "\\1.png");
            HandBackgroundpng = new Bitmap(Directory.GetCurrentDirectory() + "\\Hand_Background.png");
            Logpng = new Bitmap(Directory.GetCurrentDirectory() + "\\Log.png");
            BonoBonopng = new Bitmap(Directory.GetCurrentDirectory() + "\\BonoBono.png");
            Cow_gif = new Bitmap(Directory.GetCurrentDirectory() + ("\\Crow.gif"));
        }
        public Bitmap getBitmap(string name)
        {
            switch (name)
            {
                case "back":
                    return back;
                case "copper":
                    return copper;
                case "cellar":
                    return cellar;
                case "curse":
                    return curse;
                case "duchy":
                    return duchy;
                case "estate":
                    return estate;
                case "gold":
                    return gold;
                case "market":
                    return market;
                case "merchant":
                    return merchant;
                case "mine":
                    return mine;
                case "moat":
                    return moat;
                case "province":
                    return province;
                case "remodel":
                    return remodel;
                case "silver":
                    return silver;
                case "smithy":
                    return smithy;
                case "village":
                    return village;
                case "witch":
                    return witch;
                case "workshop":
                    return workshop;
                case "Png1":
                    return Png1;
                case "HandBackgroundpng":
                    return HandBackgroundpng;
                case "Logpng":
                    return Logpng;
                case "BonoBonopng":
                    return BonoBonopng;
                case "Cow_gif":
                    return Cow_gif;
                default:
                    return null;
            }
        }
    }
}

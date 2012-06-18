using System.Drawing;

namespace Nenetics.Examples
{
    public class ImagePhenotype : Phenotype<Bitmap>
    {
        public ImagePhenotype(Genotype genotype) : base(genotype)
        {
        }

        public ImagePhenotype(Bitmap phenotype) : base(phenotype)
        {
        }

        public override Bitmap Get()
        {
            var bmp = new Bitmap(40, 40);
            var black = Color.Black;
            var white = Color.White;
            for (int i = 0; i < 1600; i++ )
            {
                var y = i/40;
                var x = i - (y * 40);
                if(i < Genotype.Genes.Count )
                {
                    bmp.SetPixel(x, y, Genotype.Genes[i] ? white : black);
                }
                else
                {
                    bmp.SetPixel(x,y,black);
                }
            }
            return bmp;
        }

        protected override Genotype CreateGenotype(Bitmap phenotype)
        {
            var rtn = new Genotype();
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    var color = phenotype.GetPixel(j, i);
                    rtn.Genes.Add(color.Name.Contains("ffffff"));
                }
            }
            return rtn;
        }
    }
}

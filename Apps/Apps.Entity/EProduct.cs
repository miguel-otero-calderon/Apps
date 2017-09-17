using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity
{
    public class EProduct:EEntity
    {
        public string CodeProduct { get; set; }
        public string CodeProductLine { get; set; }
        public string CodeProductFamily { get; set; }
        public string CodeProductSubFamily { get; set; }
        public string CodeProductLevel { get; set; }
        public string CodeProductLevelTwo { get; set; }
        public string CodeProductType { get; set; }
        public string CodeProductGroup { get; set; }
        public string CodeProductBrand { get; set; }
        public string CodeProductUnitMeasure { get; set; }
        public string CodeProductModel { get; set; }
        public string CodeMoney { get; set; }
        public string CodeProductRecet { get; set; }
        public string CodeSunatProductTaxRV { get; set; }
        public string CodeSunatProductTaxRC { get; set; }
        public bool UseRecet { get; set; }
        public string CodeBar { get; set; }
        public string CodeIntern { get; set; }
        public string DescriptionForStore { get; set; }
        public string DescriptionForSale { get; set; }
        public string DescriptionShortForStore { get; set; }
        public double PriceCostMoneyNational { get; set; }
        public double PriceCostMoneyForeign { get; set; }
        public double PriceSaleMoneyNational { get; set; }
        public double PriceSaleMoneyForeign { get; set; }
        public double PriceCostAverage { get; set; }
        public bool ActiveForSale { get; set; }
        public bool ActiveForBuy { get; set; }
        public double StockMinimun { get; set; }
        public double StockMaximun { get; set; }
        public bool ValidateStock { get; set; }
        public short State { get; set; }
        private EAudit audit;
        public override EAudit Audit
        {
            get
            {
                if (audit == null)
                    audit = new EAudit(CodeCompany: "00", CodeEntity: "Product", Code: CodeProduct);
                return audit;
            }
            set
            {
                audit = value;
            }
        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(CodeProduct))
                throw new Exception("El Código de Product[CodeProduct] es requerido.[Product]");

            if (string.IsNullOrEmpty(CodeProductType))
                throw new Exception("El tipo de Producto[CodeProductType] es incorrecto.[Product]");

            if (string.IsNullOrEmpty(CodeProductUnitMeasure))
                throw new Exception("La unidad de medida[CodeProductUnitMeasure] es incorrecto.[Product]");

            if(UseRecet)
                if (string.IsNullOrEmpty(CodeProductRecet))
                    throw new Exception("El Código de la Receta del Producto[CodeProductRecet] es incorrecto.[Product]");

            if (string.IsNullOrEmpty(DescriptionForStore))
                throw new Exception("La descripción para Almacén del Producto[DescriptionForStore] es requerido.[Product]");

            if (string.IsNullOrEmpty(DescriptionForSale))
                throw new Exception("La descripción para Ventas[DescriptionForSale] es requerido.[Product]");

            if(PriceSaleMoneyNational < 0)
                throw new Exception("El precio de Ventas en moneda nacional[DescriptionForSale] es requerido.[Product]");

            if (PriceSaleMoneyForeign < 0)
                throw new Exception("El precio de Ventas en moneda extranjera[DescriptionForSale] es requerido.[Product]");

            if (StockMinimun < 0)
                throw new Exception("El stock mínimo no puede ser menor a cero.[Product]");

            if (StockMaximun < 0)
                throw new Exception("El stock máximo no puede ser menor a cero.[Product]");

        }
    }
}

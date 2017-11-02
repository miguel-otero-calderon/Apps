using Apps.Extension;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Apps.Entity
{
    public class EProduct : EEntity
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

        public override string CodeEntity
        {
            get
            {
                return "Product";
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

            if (UseRecet)
                if (string.IsNullOrEmpty(CodeProductRecet))
                    throw new Exception("El Código de la Receta [CodeProductRecet] es incorrecto.[Product]");

            if (string.IsNullOrEmpty(DescriptionForStore))
                throw new Exception("La descripción para Almacén del Producto[DescriptionForStore] es requerido.[Product]");

            if (string.IsNullOrEmpty(DescriptionForSale))
                throw new Exception("La descripción para Ventas[DescriptionForSale] es requerido.[Product]");

            if (PriceSaleMoneyNational < 0)
                throw new Exception("El precio de Ventas en moneda nacional[DescriptionForSale] es requerido.[Product]");

            if (PriceSaleMoneyForeign < 0)
                throw new Exception("El precio de Ventas en moneda extranjera[DescriptionForSale] es requerido.[Product]");

            if (StockMinimun < 0)
                throw new Exception("El stock mínimo no puede ser menor a cero.[Product]");

            if (StockMaximun < 0)
                throw new Exception("El stock máximo no puede ser menor a cero.[Product]");

        }
        public EProduct(DataRow dataRow, List<string> listColumns)
        {
            if (listColumns.Contains("CodeProduct") && dataRow.Validate("CodeProduct"))
                CodeProduct = Convert.ToString(dataRow["CodeProduct"]);

            if (listColumns.Contains("CodeProductLine") && dataRow.Validate("CodeProductLine"))
                CodeProductLine = Convert.ToString(dataRow["CodeProductLine"]);

            if (listColumns.Contains("CodeProductFamily") && dataRow.Validate("CodeProductFamily"))
                CodeProductFamily = Convert.ToString(dataRow["CodeProductFamily"]);

            if (listColumns.Contains("CodeProductSubFamily") && dataRow.Validate("CodeProductSubFamily"))
                CodeProductSubFamily = Convert.ToString(dataRow["CodeProductSubFamily"]);

            if (listColumns.Contains("CodeProductLevel") && dataRow.Validate("CodeProductLevel"))
                CodeProductLevel = Convert.ToString(dataRow["CodeProductLevel"]);

            if (listColumns.Contains("CodeProductLevelTwo") && dataRow.Validate("CodeProductLevelTwo"))
                CodeProductLevelTwo = Convert.ToString(dataRow["CodeProductLevelTwo"]);

            if (listColumns.Contains("CodeProductType") && dataRow.Validate("CodeProductType"))
                CodeProductType = Convert.ToString(dataRow["CodeProductType"]);

            if (listColumns.Contains("CodeProductGroup") && dataRow.Validate("CodeProductGroup"))
                CodeProductGroup = Convert.ToString(dataRow["CodeProductGroup"]);

            if (listColumns.Contains("CodeProductBrand") && dataRow.Validate("CodeProductBrand"))
                CodeProductBrand = Convert.ToString(dataRow["CodeProductBrand"]);

            if (listColumns.Contains("CodeProductUnitMeasure") && dataRow.Validate("CodeProductUnitMeasure"))
                CodeProductUnitMeasure = Convert.ToString(dataRow["CodeProductUnitMeasure"]);

            if (listColumns.Contains("CodeProductModel") && dataRow.Validate("CodeProductModel"))
                CodeProductModel = Convert.ToString(dataRow["CodeProductModel"]);

            if (listColumns.Contains("CodeMoney") && dataRow.Validate("CodeMoney"))
                CodeMoney = Convert.ToString(dataRow["CodeMoney"]);

            if (listColumns.Contains("CodeProductRecet") && dataRow.Validate("CodeProductRecet"))
                CodeProductRecet = Convert.ToString(dataRow["CodeProductRecet"]);

            if (listColumns.Contains("CodeSunatProductTaxRV") && dataRow.Validate("CodeSunatProductTaxRV"))
                CodeSunatProductTaxRV = Convert.ToString(dataRow["CodeSunatProductTaxRV"]);

            if (listColumns.Contains("CodeSunatProductTaxRC") && dataRow.Validate("CodeSunatProductTaxRC"))
                CodeSunatProductTaxRC = Convert.ToString(dataRow["CodeSunatProductTaxRC"]);

            if (listColumns.Contains("UseRecet") && dataRow.Validate("UseRecet"))
                UseRecet = Convert.ToBoolean(dataRow["UseRecet"]);

            if (listColumns.Contains("CodeBar") && dataRow.Validate("CodeBar"))
                CodeBar = Convert.ToString(dataRow["CodeBar"]);

            if (listColumns.Contains("CodeIntern") && dataRow.Validate("CodeIntern"))
                CodeIntern = Convert.ToString(dataRow["CodeIntern"]);

            if (listColumns.Contains("DescriptionForStore") && dataRow.Validate("DescriptionForStore"))
                DescriptionForStore = Convert.ToString(dataRow["DescriptionForStore"]);

            if (listColumns.Contains("DescriptionForSale") && dataRow.Validate("DescriptionForSale"))
                DescriptionForSale = Convert.ToString(dataRow["DescriptionForSale"]);

            if (listColumns.Contains("DescriptionShortForStore") && dataRow.Validate("DescriptionShortForStore"))
                DescriptionShortForStore = Convert.ToString(dataRow["DescriptionShortForStore"]);

            if (listColumns.Contains("PriceCostMoneyNational") && dataRow.Validate("PriceCostMoneyNational"))
                PriceCostMoneyNational = Convert.ToDouble(dataRow["PriceCostMoneyNational"]);

            if (listColumns.Contains("PriceCostMoneyForeign") && dataRow.Validate("PriceCostMoneyForeign"))
                PriceCostMoneyForeign = Convert.ToDouble(dataRow["PriceCostMoneyForeign"]);

            if (listColumns.Contains("PriceSaleMoneyNational") && dataRow.Validate("PriceSaleMoneyNational"))
                PriceSaleMoneyNational = Convert.ToDouble(dataRow["PriceSaleMoneyNational"]);

            if (listColumns.Contains("PriceSaleMoneyForeign") && dataRow.Validate("PriceSaleMoneyForeign"))
                PriceSaleMoneyForeign = Convert.ToDouble(dataRow["PriceSaleMoneyForeign"]);

            if (listColumns.Contains("PriceCostAverage") && dataRow.Validate("PriceCostAverage"))
                PriceCostAverage = Convert.ToDouble(dataRow["PriceCostAverage"]);

            if (listColumns.Contains("ActiveForSale") && dataRow.Validate("ActiveForSale"))
                ActiveForSale = Convert.ToBoolean(dataRow["ActiveForSale"]);

            if (listColumns.Contains("ActiveForBuy") && dataRow.Validate("ActiveForBuy"))
                ActiveForBuy = Convert.ToBoolean(dataRow["ActiveForBuy"]);

            if (listColumns.Contains("StockMinimun") && dataRow.Validate("StockMinimun"))
                StockMinimun = Convert.ToDouble(dataRow["StockMinimun"]);

            if (listColumns.Contains("StockMaximun") && dataRow.Validate("StockMaximun"))
                StockMaximun = Convert.ToDouble(dataRow["StockMaximun"]);

            if (listColumns.Contains("ValidateStock") && dataRow.Validate("ValidateStock"))
                ValidateStock = Convert.ToBoolean(dataRow["ValidateStock"]);

            if (listColumns.Contains("State") && dataRow.Validate("State"))
                State = Convert.ToInt16(dataRow["State"]);
        }

        public EProduct()
        {
        }
    }
}

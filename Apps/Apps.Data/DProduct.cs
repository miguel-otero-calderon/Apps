using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;

namespace Apps.Data
{
    public class DProduct: DData
    {
        public DataRow Select(EProduct eProduct)
        {
            DaCommand command = new DaCommand("ProductSelect");
            command.AddInParameter("@CodeProduct", DbType.String, eProduct.CodeProduct);
            return ExecuteDataRow(command);
        }
        public void Insert(EProduct eProduct)
        {
            DaCommand command = new DaCommand("ProductInsert");
            command.AddInParameter("@CodeProduct", DbType.String, eProduct.CodeProduct);
            command.AddInParameter("@CodeProductLine", DbType.String, eProduct.CodeProductLine);
            command.AddInParameter("@CodeProductFamily", DbType.String, eProduct.CodeProductFamily);
            command.AddInParameter("@CodeProductSubFamily", DbType.String, eProduct.CodeProductSubFamily);
            command.AddInParameter("@CodeProductLevel", DbType.String, eProduct.CodeProductLevel);
            command.AddInParameter("@CodeProductLevelTwo", DbType.String, eProduct.CodeProductLevelTwo);
            command.AddInParameter("@CodeProductType", DbType.String, eProduct.CodeProductType);
            command.AddInParameter("@CodeProductGroup", DbType.String, eProduct.CodeProductGroup);
            command.AddInParameter("@CodeProductBrand", DbType.String, eProduct.CodeProductBrand);
            command.AddInParameter("@CodeProductUnitMeasure", DbType.String, eProduct.CodeProductUnitMeasure);
            command.AddInParameter("@CodeProductModel", DbType.String, eProduct.CodeProductModel);
            command.AddInParameter("@CodeMoney", DbType.String, eProduct.CodeMoney);
            command.AddInParameter("@CodeProductRecet", DbType.String, eProduct.CodeProductRecet);
            command.AddInParameter("@CodeSunatProductTaxRV", DbType.String, eProduct.CodeSunatProductTaxRV);
            command.AddInParameter("@CodeSunatProductTaxRC", DbType.String, eProduct.CodeSunatProductTaxRC);
            command.AddInParameter("@UseRecet", DbType.Boolean, eProduct.UseRecet);
            command.AddInParameter("@CodeBar", DbType.String, eProduct.CodeBar);
            command.AddInParameter("@CodeIntern", DbType.String, eProduct.CodeIntern);
            command.AddInParameter("@DescriptionForStore", DbType.String, eProduct.DescriptionForStore);
            command.AddInParameter("@DescriptionForSale", DbType.String, eProduct.DescriptionForSale);
            command.AddInParameter("@DescriptionShortForStore", DbType.String, eProduct.DescriptionShortForStore);
            command.AddInParameter("@PriceCostMoneyNational", DbType.Decimal, eProduct.PriceCostMoneyNational);
            command.AddInParameter("@PriceCostMoneyForeign", DbType.Decimal, eProduct.PriceCostMoneyForeign);
            command.AddInParameter("@PriceSaleMoneyNational", DbType.Decimal, eProduct.PriceSaleMoneyNational);
            command.AddInParameter("@PriceSaleMoneyForeign", DbType.Decimal, eProduct.PriceSaleMoneyForeign);
            command.AddInParameter("@PriceCostAverage", DbType.Decimal, eProduct.PriceCostAverage);
            command.AddInParameter("@ActiveForSale", DbType.Boolean, eProduct.ActiveForSale);
            command.AddInParameter("@ActiveForBuy", DbType.Boolean, eProduct.ActiveForBuy);
            command.AddInParameter("@StockMinimun", DbType.Decimal, eProduct.StockMinimun);
            command.AddInParameter("@StockMaximun", DbType.Decimal, eProduct.StockMaximun);
            command.AddInParameter("@ValidateStock", DbType.Boolean, eProduct.ValidateStock);
            command.AddInParameter("@ValidateStock", DbType.Boolean, eProduct.ValidateStock);
            ExecuteNonQuery(command);
        }
        public void Update(EProduct eProduct)
        {
            DaCommand command = new DaCommand("ProductUpdate");
            command.AddInParameter("@CodeProduct", DbType.String, eProduct.CodeProduct);
            command.AddInParameter("@CodeProductLine", DbType.String, eProduct.CodeProductLine);
            command.AddInParameter("@CodeProductFamily", DbType.String, eProduct.CodeProductFamily);
            command.AddInParameter("@CodeProductSubFamily", DbType.String, eProduct.CodeProductSubFamily);
            command.AddInParameter("@CodeProductLevel", DbType.String, eProduct.CodeProductLevel);
            command.AddInParameter("@CodeProductLevelTwo", DbType.String, eProduct.CodeProductLevelTwo);
            command.AddInParameter("@CodeProductType", DbType.String, eProduct.CodeProductType);
            command.AddInParameter("@CodeProductGroup", DbType.String, eProduct.CodeProductGroup);
            command.AddInParameter("@CodeProductBrand", DbType.String, eProduct.CodeProductBrand);
            command.AddInParameter("@CodeProductUnitMeasure", DbType.String, eProduct.CodeProductUnitMeasure);
            command.AddInParameter("@CodeProductModel", DbType.String, eProduct.CodeProductModel);
            command.AddInParameter("@CodeMoney", DbType.String, eProduct.CodeMoney);
            command.AddInParameter("@CodeProductRecet", DbType.String, eProduct.CodeProductRecet);
            command.AddInParameter("@CodeSunatProductTaxRV", DbType.String, eProduct.CodeSunatProductTaxRV);
            command.AddInParameter("@CodeSunatProductTaxRC", DbType.String, eProduct.CodeSunatProductTaxRC);
            command.AddInParameter("@UseRecet", DbType.Boolean, eProduct.UseRecet);
            command.AddInParameter("@CodeBar", DbType.String, eProduct.CodeBar);
            command.AddInParameter("@CodeIntern", DbType.String, eProduct.CodeIntern);
            command.AddInParameter("@DescriptionForStore", DbType.String, eProduct.DescriptionForStore);
            command.AddInParameter("@DescriptionForSale", DbType.String, eProduct.DescriptionForSale);
            command.AddInParameter("@DescriptionShortForStore", DbType.String, eProduct.DescriptionShortForStore);
            command.AddInParameter("@PriceCostMoneyNational", DbType.Decimal, eProduct.PriceCostMoneyNational);
            command.AddInParameter("@PriceCostMoneyForeign", DbType.Decimal, eProduct.PriceCostMoneyForeign);
            command.AddInParameter("@PriceSaleMoneyNational", DbType.Decimal, eProduct.PriceSaleMoneyNational);
            command.AddInParameter("@PriceSaleMoneyForeign", DbType.Decimal, eProduct.PriceSaleMoneyForeign);
            command.AddInParameter("@PriceCostAverage", DbType.Decimal, eProduct.PriceCostAverage);
            command.AddInParameter("@ActiveForSale", DbType.Boolean, eProduct.ActiveForSale);
            command.AddInParameter("@ActiveForBuy", DbType.Boolean, eProduct.ActiveForBuy);
            command.AddInParameter("@StockMinimun", DbType.Decimal, eProduct.StockMinimun);
            command.AddInParameter("@StockMaximun", DbType.Decimal, eProduct.StockMaximun);
            command.AddInParameter("@ValidateStock", DbType.Boolean, eProduct.ValidateStock);
            command.AddInParameter("@ValidateStock", DbType.Boolean, eProduct.ValidateStock);
            ExecuteNonQuery(command);
        }
        public void Delete(EProduct eProduct)
        {
            DaCommand command = new DaCommand("ProductDelete");
            command.AddInParameter("@CodeProduct", DbType.String, eProduct.CodeProduct);
            ExecuteNonQuery(command);
        }

    }
}

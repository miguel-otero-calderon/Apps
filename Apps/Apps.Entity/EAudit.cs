﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Apps.Extension;

namespace Apps.Entity
{
    public class EAudit
    {
        public string CodeCompany { get; set;}
        public string CodeEntity { get; set;}
        public string Code { get; set;}
        public short Sequence { get; set;}
        public string TypeEvent { get; set;}
        public string UserRegister { get; set;}
        public DateTime DateRegister { get; set;}
        public string HostRegister { get; set;}
        protected EAudit(){}
        public void Validar()
        {
            if (string.IsNullOrEmpty(CodeCompany))
                throw new Exception("El Código de Compañia[CodeCompany] no puede ser nulo.[Audit]");

            if (string.IsNullOrEmpty(CodeEntity))
                throw new Exception("El Código de Entidad[CodeEntity] no puede ser nulo.[Audit]");

            if (string.IsNullOrEmpty(Code))
                throw new Exception("El Código[Code] no tiene valor.[Audit]");

            if (string.IsNullOrEmpty(UserRegister))
                throw new Exception("El Usuario que registra[UserRegister] no tiene valor.[Audit]");

            if (string.IsNullOrEmpty(TypeEvent))
                throw new Exception("El Tipo de Evento[TypeEvent] no tiene valor.[Audit]");
        }

        public EAudit(string CodeCompany)
        {
            this.CodeCompany = CodeCompany;
            this.CodeEntity = string.Empty;
            this.Code = string.Empty;
            Sequence = 0;
        }
        public EAudit(string CodeCompany, string CodeEntity, string Code)
        {
            this.CodeCompany = CodeCompany;
            this.CodeEntity = CodeEntity;
            this.Code = Code;
            Sequence = 0;
        }
        public EAudit(string CodeCompany, string CodeEntity, string Code,short Sequence)
        {            
            this.CodeCompany = CodeCompany;
            this.CodeEntity = CodeEntity;
            this.Code = Code;
            this.Sequence = Sequence;
        }
        public EAudit(DataRow row, List<string> columns)
        {
            if (columns.Contains("CodeCompany") && row.Validate("CodeCompany"))
                CodeCompany = Convert.ToString(row["CodeCompany"]);

            if (columns.Contains("CodeEntity") && row.Validate("CodeEntity"))
                CodeEntity = Convert.ToString(row["CodeEntity"]);

            if (columns.Contains("Code") && row.Validate("Code"))
                Code = Convert.ToString(row["Code"]);

            if (columns.Contains("Sequence") && row.Validate("Sequence"))
                Sequence = Convert.ToInt16(row["Sequence"]);

            if (columns.Contains("TypeEvent") && row.Validate("TypeEvent"))
                TypeEvent = Convert.ToString(row["TypeEvent"]);

            if (columns.Contains("UserRegister") && row.Validate("UserRegister"))
                UserRegister = Convert.ToString(row["UserRegister"]);

            if (columns.Contains("DateRegister") && row.Validate("DateRegister"))
                DateRegister = Convert.ToDateTime(row["DateRegister"]);

            if (columns.Contains("HostRegister") && row.Validate("HostRegister"))
                HostRegister = Convert.ToString(row["HostRegister"]);            
        }
    }
}

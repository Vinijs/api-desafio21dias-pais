using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public partial class Aluno
    {
        public int Id { get; set; }    
        public string Nome { get; set; }     
        public string Matricula { get; set; }

        public string Notas { get; set; }
    }

}


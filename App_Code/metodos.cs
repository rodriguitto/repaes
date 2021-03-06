﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using DatabaseModel;

[DataObject]
public class metodos{

    private DatabaseEntities1 Db = new DatabaseEntities1();

	public metodos(){
	}
    public void AddRecord(string Patente,
                          string Nombre,
                          string Apellido,
                          int Registrador){
        Db.Record.AddObject(
            new Record() { FirstName = Nombre, 
                           LastName = Apellido, 
                           Plate = Patente, 
                           IdUser=Registrador,
                           GateIn=DateTime.Now}
            );
        Db.SaveChanges();
    }
    [DataObjectMethod(DataObjectMethodType.Select)]
    public List<Record> GetRecords(){
        // and where r.plate == like %busqueda%
        return (from r in Db.Record 
                where r.GateOut == null 
                orderby r.Plate 
                select r).ToList();
    }
    [DataObjectMethod(DataObjectMethodType.Select)]
    public List<Record> GetRecordsFecha(DateTime FechaSel) {
        return (from r in Db.Record
                where r.GateIn == FechaSel
                orderby r.GateIn
                select r).ToList();
    }
    [DataObjectMethod(DataObjectMethodType.Update)]
    public void SalidaVehic(int IdVehic,DateTime GateOut) {
        Record rec = (from r in Db.Record
                      where r.IdRecord == IdVehic
                      select r).FirstOrDefault();
        rec.GateOut = GateOut;
        Db.SaveChanges();
    }
}

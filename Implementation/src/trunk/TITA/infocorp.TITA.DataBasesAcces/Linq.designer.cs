﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace infocorp.TITA.DataBasesAcces
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="baseLocal")]
	public partial class LinqDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContract(Contract instance);
    partial void UpdateContract(Contract instance);
    partial void DeleteContract(Contract instance);
    #endregion
		
		public LinqDataContext() : 
				base(global::infocorp.TITA.DataBasesAcces.Properties.Settings.Default.baseLocalConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Contract> Contracts
		{
			get
			{
				return this.GetTable<Contract>();
			}
		}
	}
	
	[Table(Name="dbo.Contracts")]
	public partial class Contract : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _id_contract;
		
		private string _site;
		
		private string _user;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onid_contractChanging(string value);
    partial void Onid_contractChanged();
    partial void OnsiteChanging(string value);
    partial void OnsiteChanged();
    partial void OnuserChanging(string value);
    partial void OnuserChanged();
    #endregion
		
		public Contract()
		{
			OnCreated();
		}
		
		[Column(Storage="_id_contract", DbType="NChar(30) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string id_contract
		{
			get
			{
				return this._id_contract;
			}
			set
			{
				if ((this._id_contract != value))
				{
					this.Onid_contractChanging(value);
					this.SendPropertyChanging();
					this._id_contract = value;
					this.SendPropertyChanged("id_contract");
					this.Onid_contractChanged();
				}
			}
		}
		
		[Column(Storage="_site", DbType="NChar(30)")]
		public string site
		{
			get
			{
				return this._site;
			}
			set
			{
				if ((this._site != value))
				{
					this.OnsiteChanging(value);
					this.SendPropertyChanging();
					this._site = value;
					this.SendPropertyChanged("site");
					this.OnsiteChanged();
				}
			}
		}
		
		[Column(Name="[user]", Storage="_user", DbType="NChar(30)")]
		public string user
		{
			get
			{
				return this._user;
			}
			set
			{
				if ((this._user != value))
				{
					this.OnuserChanging(value);
					this.SendPropertyChanging();
					this._user = value;
					this.SendPropertyChanged("user");
					this.OnuserChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591

#region License, Terms and Conditions
//
// YSWL - JSON and JSON-RPC for Microsoft .NET Framework and Mono
//
// Copyright (c) 2006-2012 YS56. All Rights Reserved.
//
// This library is free software; you can redistribute it and/or modify it under
// the terms of the GNU Lesser General Public License as published by the Free
// Software Foundation; either version 3 of the License, or (at your option)
// any later version.
//
// This library is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
// details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library; if not, write to the Free Software Foundation, Inc.,
// 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
//
#endregion

namespace YSWL.Json.Conversion.Converters
{
    #region Imports

    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;

    #endregion

    public sealed class DataRowViewExporter : ExporterBase
    {
        public DataRowViewExporter() :
            this(typeof(DataRowView)) {}

        public DataRowViewExporter(Type inputType) : 
            base(inputType) {}

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            Debug.Assert(context != null);
            Debug.Assert(value != null);
            Debug.Assert(writer != null);

            ExportRowView(context, (DataRowView) value, writer);
        }

        private static void ExportRowView(ExportContext context, DataRowView rowView, JsonWriter writer)
        {
            Debug.Assert(context != null);
            Debug.Assert(rowView != null);
            Debug.Assert(writer != null);

            writer.WriteStartObject();
    
            foreach (DataColumn column in rowView.DataView.Table.Columns)
            {
                writer.WriteMember(column.ColumnName);
                context.Export(rowView[column.Ordinal], writer);
            }
    
            writer.WriteEndObject();
        }
    }
}
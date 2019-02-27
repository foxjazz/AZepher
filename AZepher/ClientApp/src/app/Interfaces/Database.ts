
export interface ActiveTables{
  tables: Table[];
}

export interface SqlCommand {
  command: "insert" | "update" | "select" | "delete";
  tableName: string;
  columns: string[];
  values: string[];
}


export interface DbCommand {
  command: "create" | "drop";
  table: Table;
}

export interface Table{
  tableName: string;
  columns: Column[];
  watched: boolean;

}

export interface Column{
  columnName: string;
  type: "boolean" | "string" | "number" | "date";
  indexed: boolean;
}




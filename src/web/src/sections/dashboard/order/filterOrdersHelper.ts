import { GridFilterModel } from "@mui/x-data-grid";

export function getFilterOrders(filter: GridFilterModel): string | undefined {
  let _filter = filter?.items?.every((x) => x.value)
    ? filter?.items[0]?.operator
    : undefined;

  let filterField = "";
  if (_filter) {
    filterField = filter.items[0].field;

    if (filterField === "client") filterField = `${filterField}/name`;
  }

  switch (_filter) {
    case "contains":
      _filter = `${_filter}(toUpper(${filterField}), toUpper('${filter.items[0].value}'))`;
      break;
    case "equals":
    case "is":
      _filter = `${filterField} eq '${filter.items[0].value}'`;
      break;
    case "startsWith":
    case "endsWith":
      _filter = `${_filter}(toUpper(${filterField}), toUpper('${filter.items[0].value}'))`;
      break;
    default:
      _filter = undefined;
      break;
  }

  return _filter;
}

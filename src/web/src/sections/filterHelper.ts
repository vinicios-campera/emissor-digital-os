import { GridFilterModel } from "@mui/x-data-grid";

export function getFilter(filter: GridFilterModel): string | undefined {
  let _filter = filter?.items?.every((x) => x.value)
    ? filter?.items[0]?.operator
    : undefined;
  console.log(_filter);
  switch (_filter) {
    case "contains":
      _filter = `${_filter}(toUpper(${filter.items[0].field}), toUpper('${filter.items[0].value}'))`;
      break;
    case "equals":
    case "is":
      _filter = `${filter.items[0].field} eq '${filter.items[0].value}'`;
      break;
    case "startsWith":
    case "endsWith":
      _filter = `${_filter}(toUpper(${filter.items[0].field}), toUpper('${filter.items[0].value}'))`;
      break;
    default:
      _filter = undefined;
      break;
  }

  return _filter;
}

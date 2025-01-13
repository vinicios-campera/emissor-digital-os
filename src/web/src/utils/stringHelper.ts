export function removeEspecialCharacters(value: string): string {
  // eslint-disable-next-line no-useless-escape
  return value.replace(/[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/g, "");
}

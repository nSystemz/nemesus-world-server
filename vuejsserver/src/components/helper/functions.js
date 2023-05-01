//Functions
export function gueltigesDatum(datum) {
  if (!datum) return false;
  datum = datum.toString();
  datum = datum.split('.');
  if (datum.length != 3) return false;
  datum[0] = parseInt(datum[0], 10);
  datum[1] = parseInt(datum[1], 10) - 1;
  if (datum[2].length == 2) datum[2] = '20' + datum[2];
  var kontrolldatum = new Date(datum[2], datum[1], datum[0]);
  if (kontrolldatum.getDate() == datum[0] && kontrolldatum.getMonth() == datum[1] && kontrolldatum.getFullYear() == datum[2])
    return true;
  else return false;
}

export function getAge(dateString) {
  var today = new Date();
  var birthDate = new Date(dateString);
  var age = today.getFullYear() - birthDate.getFullYear();
  var m = today.getMonth() - birthDate.getMonth();
  if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
  }
  return age;
}
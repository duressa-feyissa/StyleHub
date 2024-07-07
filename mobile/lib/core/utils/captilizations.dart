abstract class Captilizations {
  static String capitalize(String s) => s[0].toUpperCase() + s.substring(1);
  static String capitalizeFirstOfEach(String s) {
    return s.split(' ').map((word) {
      return word[0].toUpperCase() + word.substring(1);
    }).join(' ');
  }
}

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.cshtml"],
  theme: {
    extend: {},
  },
  plugins: [function ({ addVariant }) {
    addVariant('child', '& > *');
    addVariant('child-hover', '& > *:hover');
    addVariant('child-active', '& > *:active');
    addVariant('child-focus', '& > *:focus');
    addVariant('li-current', '& > li.current_page_item');
}],
}

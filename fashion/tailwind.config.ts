import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    container: {
      center: true,
    },
    screens: {
      sm: "640px",
      md: "768px",
      lg: "1024px",
      xl: "1280px",
      "2xl": "1310px",
    },
    extend: {
      fontFamily: {
        Roboto: ["Roboto", "sans-serif"],
      },
      colors: {
        primary: "#EE1E80",
        black: "#1A1B21",
        primaryContainer: "#F7FBFD",
        onPrimary: "#FFFFFF",
        secondary: "#5A5D72",
        onSurface: "#1A1B21",
        onPrimaryContainer: "#06164B",
        onSurfaceVariant: "#45464F",
        surfaceContainerLow: "#F4F2FA",
      },
    },
  },
  plugins: [
    require('@tailwindcss/aspect-ratio')
  ],
};
export default config;

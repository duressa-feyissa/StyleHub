import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        Roboto: ["Roboto", "sans-serif"],
      },

      colors: {
        primary: "#EE1E80",
        primaryContainer: "#F7FBFD",
        onPrimary: "#FFFFFF",
        secondary: "#5A5D72",
        onSurface: "#1A1B21",
        onPrimaryContainer: "#06164B",
        onSurfaceVariant: "#45464F",
        surfaceContainerLow: "#F4F2FA",
      },
      spacing: {
        "xxx-small": "0px",
        "xx-small": "4px",
        "x-small": "8px",
        small: "16px",
        medium: "24px",
        large: "32px",
        "x-large": "40px",
        "xx-large": "48px",
        "xxx-large": "56px",
        "v-large": "64px",
        "vv-large": "128px",
      },
      typography: {
        "title-large-bold": {
          css: {
            fontSize: "22px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "28px",
          },
        },
        "title-large": {
          css: {
            fontSize: "22px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "28px",
          },
        },
        "title-medium": {
          css: {
            fontSize: "16px",
            fontStyle: "normal",
            fontWeight: "500",
            lineHeight: "24px",
            letterSpacing: "0.15px",
          },
        },
        "title-small": {
          css: {
            fontSize: "14px",
            fontStyle: "normal",
            fontWeight: "500",
            lineHeight: "20px",
            letterSpacing: "0.1px",
          },
        },
        "body-large": {
          css: {
            fontSize: "16px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "24px",
            letterSpacing: "0.5px",
          },
        },
        "body-medium": {
          css: {
            fontSize: "14px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "20px",
            letterSpacing: "0.25px",
          },
        },
        "body-small": {
          css: {
            fontSize: "12px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "16px",
            letterSpacing: "0.4px",
          },
        },
        "headline-large": {
          css: {
            fontSize: "32px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "40px",
          },
        },
        "headline-medium": {
          css: {
            fontSize: "28px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "36px",
          },
        },
        "headline-small": {
          css: {
            fontSize: "24px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "32px",
          },
        },
        "display-large": {
          css: {
            fontSize: "57px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "64px",
            letterSpacing: "-0.25px",
          },
        },
        "display-large-bold": {
          css: {
            fontSize: "57px",
            fontStyle: "normal",
            fontWeight: "600",
            lineHeight: "64px",
            letterSpacing: "-0.25px",
          },
        },
        "display-medium": {
          css: {
            fontSize: "45px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "52px",
          },
        },
        "display-medium-bold": {
          css: {
            fontSize: "45px",
            fontStyle: "normal",
            fontWeight: "600",
            lineHeight: "52px",
          },
        },
        "display-small": {
          css: {
            fontSize: "36px",
            fontStyle: "normal",
            fontWeight: "400",
            lineHeight: "44px",
          },
        },
      },
    },
  },
  plugins: [require("@tailwindcss/typography")],
};

export default config;

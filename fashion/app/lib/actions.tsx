"use server";
import { cookies } from "next/headers";

const host = "https://stylehub-mgow.onrender.com";

const signIn = async (type: string, formData: FormData) => {
  if (type === "credentials") {
    const { email, password } = Object.fromEntries(formData);
    const response = await fetch(`${host}/api/Authentication/Login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        loginRequest: {
          email: email,
          password: password,
        },
      }),
    });

    if (response.status === 200) {
      const data = await response.json();
      const expires = new Date(Date.now() + 10 * 1000);
      cookies().set("session", JSON.stringify(data.data), {
        expires,
        httpOnly: true,
      });
      console.log(data, "Logged in successfully");
      return data;
    } else {
      const error = await response.json();
      console.log(error.message);

      throw new Error(error.message);
    }
  }
};

export async function logout() {
  // Destroy the session
  cookies().set("session", "", { expires: new Date(0) });
}

export async function getSession() {
  const session = cookies().get("session")?.value;
  if (!session) return null;
  console.log("Session", JSON.parse(session));

  return JSON.parse(session);
}

export async function authenticate(_currentState: unknown, formData: FormData) {
  try {
    await signIn("credentials", formData);
  } catch (error) {
    if (error) {
      switch (error?.type) {
        case "CredentialsSignin":
          return "Invalid credentials.";
        default:
          return "Something went wrong.";
      }
    }
    throw error;
  }
}

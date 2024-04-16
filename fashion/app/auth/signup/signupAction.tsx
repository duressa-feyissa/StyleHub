"use server";
import { redirect } from "next/navigation";

export default async function signupAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form
  const firstName = formData.get("firstName");
  const lastName = formData.get("lastName");
  const email = formData.get("email");
  const password = formData.get("password");

  //  Send to our api route
  const res = await fetch(
    `${process.env.ROOT_URL}/api/Authentication/Register`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        registeration: {
          firstName: firstName,
          lastName: lastName,
          email: email,
          password: password,
        },
      }),
    }
  );

  const json = await res.json();

  // Redirect to login if registration is success
  if (res.ok) {
    redirect("/auth/login");
  } else {
    return json.Message;
  }
}

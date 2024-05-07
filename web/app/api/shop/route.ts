import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { type NextRequest } from "next/server";

export async function GET(request: NextRequest) {
  const response = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Shop`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  const result = await response.json();
  return Response.json(result);
}

export async function POST(request: Request) {
  const body = await request.json();
  const cookiesStore = cookies();
  const session = cookiesStore.get("session")?.value || "{}";
  const token = JSON.parse(session).token;

  console.log(body, "Shop");

  const response = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Shop`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(body),
    }
  );

  console.log(response);

  const result = await response.json();

  console.log(result);

  return Response.json(result);
}

export async function PUT(request: Request) {
  const body = await request.json();
  const cookiesStore = cookies();
  const session = cookiesStore.get("session")?.value || "{}";
  const token = JSON.parse(session).token;

  console.log(body);

  const response = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Shop`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(body),
    }
  );

  console.log(response);

  const result = await response.json();

  console.log(result);

  return Response.json(result);
}

export async function DELETE(request: Request) {
  const body = await request.json();
  const cookiesStore = cookies();
  const session = cookiesStore.get("session")?.value || "{}";
  const token = JSON.parse(session).token;

  console.log("body", body.id, process.env.BACKEND_SERVER_URL);

  const response = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Shop/${body.id}`,
    {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(body),
    }
  );

  console.log(response);

  const result = await response.json();

  console.log(result);

  return Response.json(result);
}

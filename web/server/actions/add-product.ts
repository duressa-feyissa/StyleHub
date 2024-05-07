import { productAddformSchema, shopCreateformSchema } from "@/lib/formSchema";
import { revalidatePath } from "next/cache";
import { createSafeActionClient } from "next-safe-action";

export const action = createSafeActionClient();

export const addProduct = action(productAddformSchema, async (content) => {
  
  const response = await fetch("/api/products", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(content),
  });
  const re = await response.json();
  console.log(re);

  if (!response.ok) {
    return { error: "Could not create product" };
  }

  revalidatePath("/products");
  const result = await response.json();
  return { success: result };
});

export const createShop = action(shopCreateformSchema, async (content) => {
  
  const response = await fetch("/api/shop", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(content),
  });
  const re = await response.json();
  console.log(re);

  if (!response.ok) {
    return { error: "Could not create product" };
  }

  revalidatePath("/shops");
  const result = await response.json();
  return { success: result };
});

export const fetchProducts = async () => {
  const response = await fetch("/api/products");
  return response.json();
};

export const fetchFilters = async () => {
  const response = await fetch("/api/filters");
  return response.json();
};

export const deleteProduct = async (id: string) => {
  const response = await fetch(`/api/products`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  });
  const result = await response.json();
  console.log(result);
  return result;
};

import { z } from "zod";

export const productAddformSchema = z.object({
  title: z.string().min(3, "Title is too short"),
  description: z.string().min(3, "Description is too short"),
  price: z.coerce.number(),
  target: z.string(),
  quantity: z.coerce.number(),
  condition: z.string(),
  isNegotiable: z.boolean(),
  isPublished: z.boolean(),
  city: z.string(),
  latitude: z.coerce.number(),
  longitude: z.coerce.number(),
  brandId: z.string(),
  imageIds: z.array(z.string()),
  categoryIds: z.array(z.string()),
  sizeIds: z.array(z.string()),
  colorIds: z.array(z.string()),
  materialIds: z.array(z.string()),
});

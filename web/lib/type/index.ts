export type BrandType = {
  id: string;
  name: string;
  logo: string;
  country: string;
};

export type CategoryType = {
  id: string;
  name: string;
  image: string;
};

export type ColorType = {
  id: string;
  name: string;
  hexCode: string;
};

export type ImageType = {
  id: string;
  imageUrl: string;
};

export type LocationType = {
  id: string;
  name: string;
  latitude: number;
  longitude: number;
};

export type MaterialType = {
  id: string;
  name: string;
};

export type SizeType = {
  id: string;
  name: string;
  abbreviation?: string;
};

export type ProductType = {
  id: string;
  title: string;
  description: string;
  price: number;
  quantity: number;
  target: string;
  condition: string;
  city: string;
  latitude: number;
  longitude: number;
  brand: BrandType;
  sizes: SizeType[];
  colors: ColorType[];
  materials: MaterialType[];
  categories: CategoryType[];
  images: ImageType[];
  user: UserType;
  isNegotiable: boolean;
  createdAt: string; // Date and time in ISO 8601 format
  updatedAt: string; // Date and time in ISO 8601 format
};

export type UserType = {
  id: string;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  email: string;
  latitude?: number;
  longitude?: number;
  profilePicture?: string;
  country?: string;
  city?: string;
  address?: string;
  role: RoleType[];
};

export type RoleType = {
  id: string;
  name: string;
  description: string;
  code: string;
};

// { brands, categories, colors, locations, materials, sizes }
export type FilterType = {
  brands: BrandType[];
  categories: CategoryType[];
  colors: ColorType[];
  locations: LocationType[];
  materials: MaterialType[];
  sizes: SizeType[];
};

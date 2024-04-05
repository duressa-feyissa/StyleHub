interface FeatureChipProps {
  label: string;
  isSelected: boolean;
}

export default function FeatureChip({ label, isSelected }: FeatureChipProps) {
  const color = isSelected ? "primary" : "onSurface";
  return (
    <p className={`prose-headline-small text-${color}`}>
      {label.toLocaleUpperCase()}
    </p>
  );
}

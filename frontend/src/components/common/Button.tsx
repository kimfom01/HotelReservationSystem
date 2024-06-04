interface ButtonProps {
  content: string;
}

export const Button = ({ content: children }: ButtonProps) => {
  return (
    <div className="flex justify-center">
      <button
        type="submit"
        className="bg-blue-700 text-white dark:bg-blue-600 w-fit px-8 py-2 rounded-lg focus:ring-blue-300 focus:ring-4 focus:outline-none active:ring-blue-300"
      >
        {children}
      </button>
    </div>
  );
};

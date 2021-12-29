export interface ButtonProps {
    onClick: () => void;
    children: any;
    className?: string;
    disabled?: boolean;
}

const Button: React.FC<ButtonProps> = (props: ButtonProps) => {
    const disabled = props.disabled ?? false;

    const className = props.className ??
        'btn h-10 bg-green-500 text-gray-900 rounded px-4 disabled:opacity-75 disabled:cursor-not-allowed hover:bg-green-600 hover:cursor-pointer hover:text-white';

    return <button
                className={className}
                disabled={disabled}
                onClick={props.onClick}>{props.children}</button>;
};

export default Button;
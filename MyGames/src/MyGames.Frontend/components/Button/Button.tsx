
export interface ButtonProps {
    children: any;
    onClick?: () => void;
    className?: string;
    disabled?: boolean;
    type?: 'submit' | 'reset' | 'button' | undefined;
}

const Button: React.FC<ButtonProps> = (props: ButtonProps) => {
    const disabled = props.disabled ?? false;

    const className = props.className ??
        'btn bg-green-500 text-gray-900 rounded px-4 disabled:opacity-75 disabled:cursor-not-allowed hover:bg-green-600 hover:cursor-pointer hover:text-white';

    return <button
                type={props.type ?? 'button'}
                className={className}
                disabled={disabled}
                onClick={props.onClick}>{props.children}</button>;
};

export default Button;
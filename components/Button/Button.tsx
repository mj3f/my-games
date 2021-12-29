export interface ButtonProps {
    onClick: () => void;
    className?: string;
    children: any;
}

const Button: React.FC<ButtonProps> = (props: ButtonProps) => {
    const className = props.className ??
        'btn h-10 bg-green-500 text-gray-900 rounded m-2 px-4 hover:bg-green-600 hover:cursor-pointer hover:text-white';

    return <button className={className} onClick={props.onClick}>{props.children}</button>;
};

export default Button;
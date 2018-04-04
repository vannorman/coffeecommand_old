if [ -z $1 ]; then
	printf "\n No $red argument / git commit message $nocolor supplied. Exiting \n\n"
	exit 1
fi

git add -A
git commit -m "$1"
git push

